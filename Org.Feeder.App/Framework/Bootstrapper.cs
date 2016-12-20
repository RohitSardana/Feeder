using Org.Feeder.Services;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Initializes the application built using the Composite Application Library.
    /// </summary>
    public class Bootstrapper
    {
        private readonly HostWindowFactory _hostWindowFactory;

        private IWindow _mainWindow;
        private Navigator _navigator;
        private IDbService _dbService;

        /// <summary>
        /// Initalizes the bootstrapper with HostWindowFactory
        /// </summary>
        /// <param name="hostWindowFactory"></param>
        public Bootstrapper(HostWindowFactory hostWindowFactory)
        {
            _hostWindowFactory = hostWindowFactory;
        }

        /// <summary>
        /// Initializes the application with host window
        /// </summary>
        /// <param name="appViewModel"></param>
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