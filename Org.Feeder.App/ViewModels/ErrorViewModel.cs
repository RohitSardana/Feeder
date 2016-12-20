using System;
using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Represents the view model for error screen
    /// </summary>
    public class ErrorViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes the error view model objec with error message, error title, action delegate and action tite content
        /// </summary>
        /// <param name="title">The error title</param>
        /// <param name="message">The error message</param>
        /// <param name="tryAgainAction">The action to be executed</param>
        /// <param name="actionTitle">The action button content</param>
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