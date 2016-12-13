using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.Tests.Framework
{
    [TestClass]
    public class NavigatorFixture
    {
        [TestMethod]
        public void GoingToIntro()
        {
            var appShell = new AppShellViewModel();
            var navigator = new Navigator(appShell, null);

            navigator.GoToIntro();

            Assert.IsInstanceOfType(appShell.Content, typeof(IntroViewModel));
        }

        [TestMethod]
        public void ShowingError()
        {
            const string title = "Something descriptive to the user", message = "And a message so they know what happened.";
            var recoveryAction = Substitute.For<Action>();
            var viewModel = new AppShellViewModel();
            var navigator = new Navigator(viewModel, null);

            navigator.ShowError(title, message, recoveryAction);

            var errorViewModel = (ErrorViewModel)viewModel.Content;
            Assert.AreEqual(title, errorViewModel.Title);
            Assert.AreEqual(message, errorViewModel.Message);

            errorViewModel.DismissCommand.Execute(null);
            recoveryAction.Received().Invoke();
        }

        [TestMethod]
        public void GoingToMain()
        {
            // TODO: implement this test.
            // Do not test against the real Database class because it is slow and it can fail intermittently. 

            // Given a database abstraction that will return product A and B
            // When navigating to Main screen
            // Then the application shell should display the MainViewModel
            // And the main view should show product A and B.

            // navigator.GoToMain();

            Assert.Inconclusive("TODO: Implement this test");
        }
    }
}
