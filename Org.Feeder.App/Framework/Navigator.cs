using System;
using System.Linq;
using Org.Feeder.App.ViewModels;
using Org.Feeder.FeederDb;
using PostSummary = Org.Feeder.App.Models.PostSummary;

namespace Org.Feeder.App.Framework
{
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;
        private readonly Database _database;

        public Navigator(IContentHostViewModel appShell, Database database)
        {
            _appShell = appShell;
            _database = database;
        }

        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        public void GoToMain()
        {
            var posts = _database.GetPostSummaries()
                            .Select(p => new PostSummary(p.Id, p.Title));

            Display(new MainViewModel(posts));
        }

        public void ShowError(string title, string message, Action recoveryAction)
        {
            Display(new ErrorViewModel(title, message, recoveryAction));
        }

        private void Display<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            _appShell.Content = viewModel;
        }
    }
}