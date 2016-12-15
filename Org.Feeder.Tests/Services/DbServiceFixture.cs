using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.App.Services;
using Org.Feeder.App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Org.Feeder.Tests.Services
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
                        Assert.IsNotNull(userResult.Data);
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
    }
}
