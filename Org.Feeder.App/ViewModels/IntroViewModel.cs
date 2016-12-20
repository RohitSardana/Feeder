using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Represents view model for the Introduction screen
    /// </summary>
    public class IntroViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;

        /// <summary>
        /// Initializes the IntroViewModel with navigator and start command
        /// </summary>
        /// <param name="navigator">The navigator</param>
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