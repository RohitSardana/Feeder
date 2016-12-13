using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.Tests.Framework
{
    [TestClass]
    public class BootstrapperFixture
    {
        [TestMethod]
        public void Initializing()
        {
            // Arrange
            var hostWindow = Substitute.For<IWindow>();
            var viewModel = new AppShellViewModel();
            var factory = Substitute.For<HostWindowFactory>();
            factory.CreateHostWindow(viewModel).Returns(hostWindow);
            var bootstrapper = new Bootstrapper(factory);

            // Act
            bootstrapper.Initialize(viewModel);

            // Assert
            hostWindow.Received().Show();
            Assert.IsInstanceOfType(viewModel.Content, typeof(IntroViewModel));
        }
    }
}