using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.Models;
using System.Collections.Generic;
using System.Linq;

namespace Org.Feeder.Services.Tests
{
    [TestClass]
    public class DbServiceFixture
    {
        [TestMethod]
        public void GettingPostSummaries()
        {
            //Arrange
            IDbService dbService = new DbService();

            //Act
            KnownResult<IEnumerable<PostSummary>> postSummariesResult = dbService.GetPostSummaries();

            //Assert
            if (postSummariesResult.HasError == false)
            {
                Assert.IsNotNull(postSummariesResult.Data);
                Assert.IsTrue(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
            else if (postSummariesResult.HasError)
            {
                Assert.IsNull(postSummariesResult.Data);
                Assert.IsFalse(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
        }

        [TestMethod]
        public void GettingPost()
        {
            //Arrange
            IDbService dbService = new DbService();

            //Act and Assert
            KnownResult<IEnumerable<PostSummary>> postSummariesResult = dbService.GetPostSummaries();

            if (postSummariesResult.HasError == false)
            {
                Assert.IsNotNull(postSummariesResult.Data);
                Assert.IsTrue(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));

                PostSummary postSummary = postSummariesResult.Data.FirstOrDefault();
                KnownResult<FeederDb.Post> postResult = dbService.GetPostById(postSummary.PostId);
                if (postResult.HasError == false)
                {
                    Assert.IsNotNull(postResult.Data);
                    Assert.IsTrue(String.IsNullOrWhiteSpace(postResult.ErrorMessage));
                }
                else if (postResult.HasError)
                {
                    Assert.IsNull(postResult.Data);
                    Assert.IsFalse(String.IsNullOrWhiteSpace(postResult.ErrorMessage));
                }
            }
            else if (postSummariesResult.HasError)
            {
                Assert.IsNull(postSummariesResult.Data);
                Assert.IsFalse(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
        }

        [TestMethod]
        public void GettingUser()
        {
            //Arrange
            IDbService dbService = new DbService();

            //Act and Assert
            KnownResult<IEnumerable<PostSummary>> postSummariesResult = dbService.GetPostSummaries();

            if (postSummariesResult.HasError == false)
            {
                Assert.IsNotNull(postSummariesResult.Data);
                Assert.IsTrue(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));

                PostSummary postSummary = postSummariesResult.Data.FirstOrDefault();
                KnownResult<FeederDb.Post> postResult = dbService.GetPostById(postSummary.PostId);
                if (postResult.HasError)
                {
                    Assert.IsNull(postResult.Data);
                    Assert.IsFalse(String.IsNullOrWhiteSpace(postResult.ErrorMessage));
                }
                else if (postResult.HasError == false)
                {
                    Assert.IsNotNull(postResult.Data);
                    Assert.IsTrue(String.IsNullOrWhiteSpace(postResult.ErrorMessage));

                    KnownResult<FeederDb.User> userResult = dbService.GetUserById(postResult.Data.UserId);
                    if (userResult.HasError)
                    {
                        Assert.IsNull(userResult.Data);
                        Assert.IsFalse(String.IsNullOrWhiteSpace(userResult.ErrorMessage));
                    }
                    else
                    {
                        Assert.IsNotNull(userResult.Data);
                        Assert.IsTrue(String.IsNullOrWhiteSpace(userResult.ErrorMessage));
                    }
                }
            }
            else if (postSummariesResult.HasError)
            {
                Assert.IsNull(postSummariesResult.Data);
                Assert.IsFalse(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
        }

        [TestMethod]
        public void GettingComments()
        {
            //Arrange
            IDbService dbService = new DbService();

            //Act and Assert
            KnownResult<IEnumerable<PostSummary>> postSummariesResult = dbService.GetPostSummaries();

            if (postSummariesResult.HasError == false)
            {
                Assert.IsNotNull(postSummariesResult.Data);
                Assert.IsTrue(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));

                PostSummary postSummary = postSummariesResult.Data.FirstOrDefault();
                KnownResult<IList<CommentSummary>> commentsResult = dbService.GetCommentSummaryByPostId(postSummary.PostId);
                if (commentsResult.HasError)
                {
                    Assert.IsNull(commentsResult.Data);
                    Assert.IsFalse(String.IsNullOrWhiteSpace(commentsResult.ErrorMessage));
                }
                else if (commentsResult.HasError == false)
                {
                    Assert.IsNotNull(commentsResult.Data);
                    Assert.IsTrue(String.IsNullOrWhiteSpace(commentsResult.ErrorMessage));
                }

            }
            else if (postSummariesResult.HasError)
            {
                Assert.IsNull(postSummariesResult.Data);
                Assert.IsFalse(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
        }

        /*Iterating through GettingUsers multiple times, 
        may increase the chances of getting timeout errors from feederDb, 
        thus increasing code coverage*/
        [TestMethod]
        public void GettingUser_MultipleTimes()
        {
            for (int count = 0; count <= 10; count++)
            {
                GettingUser();
            }
        }

        [TestMethod]
        public void GettingPost_MultipleTimes()
        {
            for (int count = 0; count <= 10; count++)
            {
                GettingPost();
            }
        }

        [TestMethod]
        public void GettingPostSummaries_MultipleTimes()
        {
            for (int count = 0; count <= 10; count++)
            {
                GettingPostSummaries();
            }
        }

        [TestMethod]
        public void GettingComments_MultipleTimes()
        {
            for (int count = 0; count <= 10; count++)
            {
                GettingComments();
            }
        }
    }
}
