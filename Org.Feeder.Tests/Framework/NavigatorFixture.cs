using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;
using System.Threading;
using Org.Feeder.Models;
using System.Collections.Generic;
using Org.Feeder.Services;
using System.Linq;
using Rhino.Mocks;

namespace Org.Feeder.Tests.Framework
{
    [TestClass]
    public class NavigatorFixture
    {
        private ManualResetEvent initializedSignal;

        [TestInitialize]
        public void Init()
        {
            initializedSignal = new ManualResetEvent(true);
        }

        [TestMethod]
        public void GoingToIntro()
        {
            var appShell = new AppShellViewModel();
            var navigator = new Navigator(appShell,null);

            navigator.GoToIntro();

            Assert.IsInstanceOfType(appShell.Content, typeof(IntroViewModel));
        }

        [TestMethod]
        public void ShowingError()
        {
            const string title = "Something descriptive to the user", message = "And a message so they know what happened.";
            var recoveryAction = Substitute.For<Action>();
            var viewModel = new AppShellViewModel();
            var navigator = new Navigator(viewModel,null);

            navigator.ShowError(title, message, recoveryAction, "Retry");

            var errorViewModel = (ErrorViewModel)viewModel.Content;
            Assert.AreEqual(title, errorViewModel.Title);
            Assert.AreEqual(message, errorViewModel.Message);

            errorViewModel.ActionCommand.Execute(null);
            recoveryAction.Received().Invoke();
        }

        [TestMethod]
        public void GoingToMain()
        {
            //Arrange
            var appShell = new AppShellViewModel();
            IDbService _dbService = MockRepository.GenerateMock<IDbService>();
            var navigator = new Navigator(appShell, _dbService);
           
            KnownResult<IEnumerable<PostSummary>> postResult = new KnownResult<IEnumerable<PostSummary>>();
            postResult.Data = (from id in Enumerable.Range(1, 2)
                               select new PostSummary(id, "Post " + id)).ToList();
            _dbService.Stub(x => x.GetPostSummaries()).Return(postResult);

            //Act
            navigator.GoToMain();

            //Assert
            Assert.IsInstanceOfType(appShell.Content, typeof(MainViewModel));
            MainViewModel mainViewModel = appShell.Content as MainViewModel;
            Assert.IsNotNull(mainViewModel);
            if (mainViewModel.Posts == null || mainViewModel.Posts.Count == 0)
            {
                initializedSignal = new ManualResetEvent(false);
                mainViewModel.OnInitialized += MainViewModel_OnInitialized;
            }
            initializedSignal.WaitOne(5000);
            CollectionAssert.AreEquivalent(
             new[] { "Post 1", "Post 2" },
             mainViewModel.Posts.Select(x => x.Title).ToArray());
        }

        [TestMethod]
        public void GoingToDetailedScreen()
        {
            var appShell = new AppShellViewModel();
            IDbService _dbService = MockRepository.GenerateMock<IDbService>();
            var navigator = new Navigator(appShell, _dbService);
           
            int postId = 1;//Any random post id.
            navigator.GoToDetailedPost(postId);
            Assert.IsInstanceOfType(appShell.Content, typeof(DetailedPostViewModel));
        }

        private void MainViewModel_OnInitialized()
        {
            initializedSignal.Set();
        }
    }
}
