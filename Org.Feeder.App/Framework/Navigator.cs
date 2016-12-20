using System;
using Org.Feeder.App.ViewModels;
using Org.Feeder.Services;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Implementation of <see cref="Org.Feeder.App.Framework.INavigator"/> to provide methods to navigate user from one screen to another
    /// </summary>
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;
        private readonly IDbService _dbService;

        /// <summary>
        /// Initializes the Navigator with application shell and Db service
        /// </summary>
        /// <param name="appShell"></param>
        /// <param name="dbService"></param>
        public Navigator(IContentHostViewModel appShell, IDbService dbService)
        {
            _appShell = appShell;
            _dbService = dbService;
        }

        /// <summary>
        /// Navigates the user to Introduction screen.
        /// </summary>
        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        /// <summary>
        /// Navigates the user to Main Screen.
        /// </summary>
        public void GoToMain()
        {
            Display(new MainViewModel(this, _dbService));
        }

        /// <summary>
        /// Navigates user to Error screen
        /// </summary>
        /// <param name="title">The title of the error popup</param>
        /// <param name="message">The error message</param>
        /// <param name="recoveryAction">Action to be executed on confirmation of error popup</param>
        /// <param name="actionTitle">Action button content</param>
        public void ShowError(string title, string message, Action recoveryAction, string actionTitle)
        {
            Display(new ErrorViewModel(title, message, recoveryAction, actionTitle));
        }

        /// <summary>
        /// Navigates the user to Detailed post screen.
        /// </summary>
        /// <param name="postId">The post id for which detailed information to be shown</param>
        public void GoToDetailedPost(int postId)
        {
            Display(new DetailedPostViewModel(this, _dbService, postId));
        }

        private void Display<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            _appShell.Content = viewModel;
        }
    }
}