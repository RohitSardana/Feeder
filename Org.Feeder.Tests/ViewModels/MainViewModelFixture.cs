using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.App.Models;
using Org.Feeder.App.ViewModels;
using System.Threading;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Services;
using Rhino.Mocks;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelFixture
    {
        private List<PostSummary> _posts;
        private MainViewModel _viewModel;
        private ManualResetEvent _waitingEvent;
        private AppShellViewModel appShell;

        [TestInitialize]
        public void Init()
        {
            _waitingEvent = new ManualResetEvent(false);
            appShell = new AppShellViewModel();
            IDbService _dbService = MockRepository.GenerateMock<IDbService>();
            var navigator = new Navigator(appShell, _dbService);

            KnownResult<IEnumerable<PostSummary>> postResult = new KnownResult<IEnumerable<PostSummary>>();
            postResult.Data = _posts= (from id in Enumerable.Range(1, 12)
                               select new PostSummary(id, "Post " + id)).ToList(); ;
            _dbService.Stub(x => x.GetPostSummaries()).Return(postResult);
            _viewModel = new MainViewModel(navigator, _dbService);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
        }

        [TestMethod]
        public void Filtering()
        {
            //Act
            _waitingEvent.WaitOne();
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
            var selectedPost = _posts.Skip(5).First();

            _viewModel.SelectCommand.Execute(selectedPost);

            Assert.Inconclusive("BONUS: implement this test assert it navigates to the corresponding details screen.");
        }

        private void _viewModel_OnInitialized()
        {
            _waitingEvent.Set();
        }
    }
}