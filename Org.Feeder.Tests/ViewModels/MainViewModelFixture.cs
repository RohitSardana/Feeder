using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.Models;
using Org.Feeder.App.ViewModels;
using System.Threading;
using Org.Feeder.App.Framework;
using Org.Feeder.Services;
using Rhino.Mocks;
using System;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelFixture
    {
        private MainViewModel _viewModel;
        private ManualResetEvent _waitingEvent;
        private AppShellViewModel appShell;
        private IDbService _dbService;
        private INavigator _navigator;

        [TestInitialize]
        public void Init()
        {
            appShell = new AppShellViewModel();
            _dbService = MockRepository.GenerateMock<IDbService>();
            _navigator = new Navigator(appShell, _dbService);
            _waitingEvent = new ManualResetEvent(false);
        }

        [TestMethod]
        public void Filtering()
        {
            //Arrange
            KnownResult<IEnumerable<PostSummary>> postResult = new KnownResult<IEnumerable<PostSummary>>();
            postResult.Data = (from id in Enumerable.Range(1, 12) select new PostSummary(id, "Post " + id)).ToList(); ;
            _dbService.Stub(x => x.GetPostSummaries()).Return(postResult);

            //Act
            _viewModel = new MainViewModel(_navigator, _dbService);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            _waitingEvent.WaitOne(5000);
            _viewModel.FilterCommand.Execute("Post 1");

            //Assert
            Assert.IsFalse(_viewModel.IsBusy);
            CollectionAssert.AreEquivalent(
                new[] { "Post 1", "Post 10", "Post 11", "Post 12" },
                _viewModel.Posts.Select(x => x.Title).ToArray());
        }

        [TestMethod]
        public void SelectingPost()
        {
            //Arrange
            KnownResult<IEnumerable<PostSummary>> postResult = new KnownResult<IEnumerable<PostSummary>>();
            postResult.Data = (from id in Enumerable.Range(1, 12) select new PostSummary(id, "Post " + id)).ToList(); ;
            _dbService.Stub(x => x.GetPostSummaries()).Return(postResult);

            //Act
            _viewModel = new MainViewModel(_navigator, _dbService);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            _waitingEvent.WaitOne(5000);
            var selectedPost = _viewModel.Posts.Skip(5).First();
            _viewModel.SelectCommand.Execute(selectedPost);

            //Assert
            Assert.IsTrue(selectedPost.PostId == 6);
            Assert.IsTrue(String.Compare(selectedPost.Title, "Post 6", false) == 0);
            Assert.IsInstanceOfType(appShell.Content, typeof(DetailedPostViewModel));
        }

        [TestMethod]
        public void MainScreen_OperationUnderProcessing()
        {
            //Arrange
            int operationTimeInMilliseconds = 5000;
            int waitTimeInMilliseconds = 8000;
            int breathingTimeForCtor = 2000;
            _dbService = MockRepository.GenerateMock<IDbService>();
            _navigator = new Navigator(appShell, _dbService);
            KnownResult<IEnumerable<PostSummary>> postResult = new KnownResult<IEnumerable<PostSummary>>();
            postResult.Data = (from id in Enumerable.Range(1, 12) select new PostSummary(id, "Post " + id)).ToList(); ;
            _dbService.Stub(x => x.GetPostSummaries()).WhenCalled(x=>Thread.Sleep(operationTimeInMilliseconds)).Return(postResult);

            //Act
            _viewModel = new MainViewModel(_navigator, _dbService);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            Thread.Sleep(breathingTimeForCtor);

            //Assert
            Assert.IsTrue(_viewModel.IsBusy);
            _waitingEvent.WaitOne(waitTimeInMilliseconds);
            Assert.IsFalse(_viewModel.IsBusy);
        }

        private void _viewModel_OnInitialized()
        {
            _waitingEvent.Set();
        }
    }
}