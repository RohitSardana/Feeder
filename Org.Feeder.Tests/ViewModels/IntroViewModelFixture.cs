using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;
using Org.Feeder.App.Services;
using Org.Feeder.App.ViewModels;
using Rhino.Mocks;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class IntroViewModelFixture
    {
        [TestMethod]
        public void Starting()
        {
            var navigator = MockRepository.GenerateMock<INavigator>();
            var viewModel = new IntroViewModel(navigator);

            viewModel.StartCommand.Execute(null);

            navigator.AssertWasCalled(x => x.GoToMain());
        }
    }
}