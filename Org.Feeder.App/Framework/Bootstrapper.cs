using Org.Feeder.Services;

namespace Org.Feeder.App.Framework
{
    public class Bootstrapper
    {
        private readonly HostWindowFactory _hostWindowFactory;

        private IWindow _mainWindow;
        private Navigator _navigator;
        private IDbService _dbService;

        public Bootstrapper(HostWindowFactory hostWindowFactory)
        {
            _hostWindowFactory = hostWindowFactory;
        }

        public void Initialize(IContentHostViewModel appViewModel)
        {
            _dbService = new DbService();
            _navigator = new Navigator(appViewModel, _dbService);

            _mainWindow = _hostWindowFactory.CreateHostWindow(appViewModel);
            _mainWindow.Show();

            _navigator.GoToIntro();
        }
    }
}