using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.App.Services;
using Org.Feeder.App.Models;
using System.Collections.Generic;
using System.Linq;
using FeederDb = Org.Feeder.FeederDb;

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
                Assert.IsFalse(String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage));
            }
        }
    }
}
