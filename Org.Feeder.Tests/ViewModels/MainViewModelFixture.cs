using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.App.Models;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelFixture
    {
        private List<PostSummary> _posts;
        private MainViewModel _viewModel;

        [TestInitialize]
        public void Init()
        {
            _posts = (from id in Enumerable.Range(1, 12)
                      select new PostSummary(id, "Post " + id)).ToList();

            _viewModel = new MainViewModel(_posts);
        }

        [TestMethod]
        public void Filtering()
        {
            _viewModel.FilterCommand.Execute("Post 1");

            CollectionAssert.AreEquivalent(
                new[] {"Post 1", "Post 10", "Post 11", "Post 12"},
                _viewModel.Posts.Select(x => x.Title).ToArray());
        }

        [TestMethod]
        public void SelectingPost()
        {
            var selectedPost = _posts.Skip(5).First();

            _viewModel.SelectCommand.Execute(selectedPost);

            Assert.Inconclusive("BONUS: implement this test assert it navigates to the corresponding details screen.");
        }
    }
}