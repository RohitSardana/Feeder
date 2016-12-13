using System;
using Org.Feeder.App.ViewModels;
using Org.Feeder.App.Services;

namespace Org.Feeder.App.Framework
{
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;
        private readonly IDbService _dbService;

        public Navigator(IContentHostViewModel appShell, IDbService dbService)
        {
            _appShell = appShell;
            _dbService = dbService;
        }

        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        public void GoToMain()
        {
            Display(new MainViewModel(this, _dbService));
        }

        public void ShowError(string title, string message, Action recoveryAction, string actionTitle)
        {
            Display(new ErrorViewModel(title, message, recoveryAction, actionTitle));
        }

        private void Display<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            _appShell.Content = viewModel;
        }
    }
}