using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.Models;
using Org.Feeder.App.ViewModels;
using Rhino.Mocks;
using Org.Feeder.App.Framework;
using Org.Feeder.Services;
using System.Threading;
using System;
using Org.Feeder.Common;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class DetailedViewModelFixture
    {
        private DetailedPostViewModel _viewModel;
        private AppShellViewModel appShell;
        private IDbService _dbService;
        private INavigator _navigator;
        private ManualResetEvent _waitingEvent;
        [TestInitialize]
        public void Init()
        {
            appShell = new AppShellViewModel();
            _dbService = MockRepository.GenerateMock<IDbService>();
            _navigator = new Navigator(appShell, _dbService);
            _waitingEvent = new ManualResetEvent(false);
        }

        [TestMethod]
        public void DisplayingDetailedPost()
        {
            //Arrange
            string postBody = "This is the test post";
            string postTitle = "Post 1";
            int postId = 1;
            int userId = 1;
            string userName = "User 1";
            string userEmail = "User1@test.com";
            string userImageUrl = "User1ImageUrl";
            string commenter1Name = "Commenter 1";
            string commenter1Text = "Comment Text 1";
            string commenter2Name = "Commenter 2";
            string commenter2Text = "Comment Text 2";

            #region Simulating dbService to return the predefined data.

            KnownResult<FeederDb.Post> postResult = new KnownResult<FeederDb.Post>();
            postResult.Data = new FeederDb.Post(postId, userId, postTitle, postBody);
            _dbService.Stub(x => x.GetPostById(postId)).Return(postResult);

            KnownResult<FeederDb.User> userResult = new KnownResult<FeederDb.User>();
            userResult.Data = new FeederDb.User(userId, userName, userEmail, userImageUrl);
            _dbService.Stub(x => x.GetUserById(postId)).Return(userResult);

            KnownResult<IList<CommentSummary>> commentsResult = new KnownResult<IList<CommentSummary>>();
            CommentSummary comment1 = new CommentSummary(commenter1Name, commenter1Text);
            CommentSummary comment2 = new CommentSummary(commenter2Name, commenter2Text);
            commentsResult.Data = new List<CommentSummary>() { comment1, comment2 };
            _dbService.Stub(x => x.GetCommentSummaryByPostId(postId)).Return(commentsResult);

            #endregion

            //Act
            _viewModel = new DetailedPostViewModel(_navigator, _dbService, postId);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            // wait either for 5 seconds or for viewModel data to be initialized, whichever occurs first.
            _waitingEvent.WaitOne(5000);

            //Assert
            Assert.IsTrue(String.Compare(_viewModel.Title, postTitle, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Body, postBody, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Author, userName, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.ImageUrl, userImageUrl, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.CommentHeaderText,Messages.CommentsHeaderText , false) == 0);
            Assert.IsFalse(_viewModel.IsBusy);
            CollectionAssert.AreEquivalent(
                new[] { commenter1Name, commenter2Name },
                _viewModel.Comments.Select(x => x.CommenterName).ToArray());
            CollectionAssert.AreEquivalent(
              new[] { commenter1Text, commenter2Text },
              _viewModel.Comments.Select(x => x.Text).ToArray());
        }

        [TestMethod]
        public void DisplayingDetailedPost_PostFetchFailed()
        {
            //Arrange
            int postId = 1;

            #region Simulating dbService to return the predefined data.

            KnownResult<FeederDb.Post> postResult = new KnownResult<FeederDb.Post>();
            postResult.Data = null;
            postResult.ErrorMessage = Messages.CouldNotFetchPostDetails;
            _dbService.Stub(x => x.GetPostById(postId)).Return(postResult);

            #endregion

            //Act
            _viewModel = new DetailedPostViewModel(_navigator, _dbService, postId);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            // wait either for 5 seconds or for viewModel data to be initialized, whichever occurs first.
            _waitingEvent.WaitOne(5000);

            //Assert
            Assert.IsInstanceOfType(appShell.Content, typeof(ErrorViewModel));
        }

        [TestMethod]
        public void DisplayingDetailedPost_UserFetchFailed()
        {
            //Arrange
            string postBody = "This is the test post";
            string postTitle = "Post 1";
            int postId = 1;
            int userId = 1;
            string commenter1Name = "Commenter 1";
            string commenter1Text = "Comment Text 1";
            string commenter2Name = "Commenter 2";
            string commenter2Text = "Comment Text 2";

            #region Simulating dbService to return the predefined data.

            KnownResult<FeederDb.Post> postResult = new KnownResult<FeederDb.Post>();
            postResult.Data = new FeederDb.Post(postId, userId, postTitle, postBody);
            _dbService.Stub(x => x.GetPostById(postId)).Return(postResult);

            KnownResult<FeederDb.User> userResult = new KnownResult<FeederDb.User>();
            userResult.Data = null;
            userResult.ErrorMessage = Messages.CouldNotFetchUserDetails;
            _dbService.Stub(x => x.GetUserById(postId)).Return(userResult);

            KnownResult<IList<CommentSummary>> commentsResult = new KnownResult<IList<CommentSummary>>();
            CommentSummary comment1 = new CommentSummary(commenter1Name, commenter1Text);
            CommentSummary comment2 = new CommentSummary(commenter2Name, commenter2Text);
            commentsResult.Data = new List<CommentSummary>() { comment1, comment2 };
            _dbService.Stub(x => x.GetCommentSummaryByPostId(postId)).Return(commentsResult);

            #endregion

            //Act
            _viewModel = new DetailedPostViewModel(_navigator, _dbService, postId);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            // wait either for 5 seconds or for viewModel data to be initialized, whichever occurs first.
            _waitingEvent.WaitOne(5000);

            //Assert
            Assert.IsTrue(String.Compare(_viewModel.Title, postTitle, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Body, postBody, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Author, Messages.NotAvailable, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.ImageUrl, Messages.NotAvailable, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.CommentHeaderText, Messages.CommentsHeaderText, false) == 0);
            Assert.IsFalse(_viewModel.IsBusy);
            CollectionAssert.AreEquivalent(
                new[] { commenter1Name, commenter2Name },
                _viewModel.Comments.Select(x => x.CommenterName).ToArray());
            CollectionAssert.AreEquivalent(
              new[] { commenter1Text, commenter2Text },
              _viewModel.Comments.Select(x => x.Text).ToArray());
        }

        [TestMethod]
        public void DisplayingDetailedPost_CommentsFetchFailed()
        {
            //Arrange
            string postBody = "This is the test post";
            string postTitle = "Post 1";
            int postId = 1;
            int userId = 1;
            string userName = "User 1";
            string userEmail = "User1@test.com";
            string userImageUrl = "User1ImageUrl";

            #region Simulating dbService to return the predefined data.

            KnownResult<FeederDb.Post> postResult = new KnownResult<FeederDb.Post>();
            postResult.Data = new FeederDb.Post(postId, userId, postTitle, postBody);
            _dbService.Stub(x => x.GetPostById(postId)).Return(postResult);

            KnownResult<FeederDb.User> userResult = new KnownResult<FeederDb.User>();
            userResult.Data = new FeederDb.User(userId, userName, userEmail, userImageUrl);
            _dbService.Stub(x => x.GetUserById(postId)).Return(userResult);

            KnownResult<IList<CommentSummary>> commentsResult = new KnownResult<IList<CommentSummary>>();
            commentsResult.Data = null;
            _dbService.Stub(x => x.GetCommentSummaryByPostId(postId)).Return(commentsResult);

            #endregion

            //Act
            _viewModel = new DetailedPostViewModel(_navigator, _dbService, 1);
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            // wait either for 5 seconds or for viewModel data to be initialized, whichever occurs first.
            _waitingEvent.WaitOne(5000);

            //Assert
            Assert.IsTrue(String.Compare(_viewModel.Title, postTitle, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Body, postBody, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.Author, userName, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.ImageUrl, userImageUrl, false) == 0);
            Assert.IsTrue(String.Compare(_viewModel.CommentHeaderText, Messages.CommentsNotAvailable, false) == 0);
            Assert.IsFalse(_viewModel.IsBusy);
        }

        [TestMethod]
        public void GoingBackToMainScreen()
        {
            //Act
            _viewModel = new DetailedPostViewModel(_navigator, _dbService, 1);// Any random post id
            _viewModel.OnInitialized += _viewModel_OnInitialized;
            // wait either for 5 seconds or for viewModel data to be initialized, whichever occurs first.
            _waitingEvent.WaitOne(5000);
            _viewModel.GoBackCommand.Execute(null);

            //Assert
            Assert.IsInstanceOfType(appShell.Content, typeof(MainViewModel));
        }

        private void _viewModel_OnInitialized()
        {
            _waitingEvent.Set();
        }
    }
}