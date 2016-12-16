using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Org.Feeder.Models.Test
{
    [TestClass]
    public class KnownResultTest
    {
        [TestMethod]
        public void TestingKnownResult_ValidData()
        {
            //Arrange and Act
            KnownResult<PostSummary> postSummaryResult = new KnownResult<PostSummary>();
            postSummaryResult.Data = new PostSummary(1, "Post 1");

            //Assert
            Assert.IsTrue(String.IsNullOrWhiteSpace(postSummaryResult.ErrorMessage));
            Assert.IsFalse(postSummaryResult.HasError);
        }

        [TestMethod]
        public void TestingKnownResult_InValidData()
        {
            //Arrange and Act
            KnownResult<PostSummary> postSummaryResult = new KnownResult<PostSummary>();
            postSummaryResult.ErrorMessage = "Error occured.";

            //Assert
            Assert.IsTrue(postSummaryResult.HasError);
        }
    }
}
