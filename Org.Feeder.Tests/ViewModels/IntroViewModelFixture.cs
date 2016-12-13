using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class IntroViewModelFixture
    {
        [TestMethod]
        public void Starting()
        {
            var navigator = Substitute.For<INavigator>();
            var viewModel = new IntroViewModel(navigator);

            viewModel.StartCommand.Execute(null);

            // NSubstitute allows you to assert the navigator was called with the method `Received()`
            navigator.Received().GoToMain();
        }
    }
}