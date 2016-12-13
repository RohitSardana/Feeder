using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;

namespace Org.Feeder.App.ViewModels
{
    public class IntroViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;

        public IntroViewModel(INavigator navigator)
        {
            _navigator = navigator;
            StartCommand = new ActionCommand(Start);
        }

        public ActionCommand StartCommand { get; private set; }

        private void Start()
        {
            _navigator.GoToMain();
        }
    }
}