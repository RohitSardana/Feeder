using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Introduction view model of the application
    /// </summary>
    public class IntroViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;

        /// <summary>
        /// Initializes the IntroViewModel.
        /// </summary>
        /// <param name="navigator"></param>
        public IntroViewModel(INavigator navigator)
        {
            _navigator = navigator;
            StartCommand = new ActionCommand(Start);
        }

        /// <summary>
        /// A command to execute the Start method.
        /// </summary>
        public ActionCommand StartCommand { get; private set; }

        /// <summary>
        /// Navigates user to the main screen.
        /// </summary>
        private void Start()
        {
            _navigator.GoToMain();
        }
    }
}