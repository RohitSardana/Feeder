using System.Windows;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.App
{
    public partial class App
    {
        private Bootstrapper _bootstrapper;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _bootstrapper = new Bootstrapper(new HostWindowFactory());
            _bootstrapper.Initialize(new AppShellViewModel());
        }
    }
}
