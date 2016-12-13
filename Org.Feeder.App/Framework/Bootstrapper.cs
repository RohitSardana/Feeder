using Org.Feeder.FeederDb;

namespace Org.Feeder.App.Framework
{
    public class Bootstrapper
    {
        private readonly HostWindowFactory _hostWindowFactory;

        private IWindow _mainWindow;
        private Navigator _navigator;
        private Database _database;

        public Bootstrapper(HostWindowFactory hostWindowFactory)
        {
            _hostWindowFactory = hostWindowFactory;
        }

        public void Initialize(IContentHostViewModel appViewModel)
        {
            _database = new Database(ConnectionStrings.Default);

            _navigator = new Navigator(appViewModel, _database);

            _mainWindow = _hostWindowFactory.CreateHostWindow(appViewModel);
            _mainWindow.Show();

            _navigator.GoToIntro();
        }
    }
}