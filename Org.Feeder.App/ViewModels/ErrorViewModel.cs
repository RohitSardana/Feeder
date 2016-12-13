using System;
using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Persists the error data.
    /// </summary>
    public class ErrorViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes the error data.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="tryAgainAction"></param>
        /// <param name="actionTitle"></param>
        public ErrorViewModel(string title, string message, Action tryAgainAction, string actionTitle)
        {
            Title = title;
            Message = message;
            ActionTitle = actionTitle;
            ActionCommand = new ActionCommand(
                tryAgainAction);
        }

        /// <summary>
        /// Gets the title of the error.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the button text
        /// </summary>
        public string ActionTitle { get; private set; }
        /// <summary>
        /// Gets the error message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Executes the action on button click
        /// </summary>
        public ActionCommand ActionCommand { get; private set; }
    }
}