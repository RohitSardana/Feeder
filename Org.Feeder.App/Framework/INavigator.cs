using System;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Interface provides methods to navigate from one screen to another within the application.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Navigates user to Introduction screen
        /// </summary>
        void GoToIntro();

        /// <summary>
        /// Navigates user to Main screen
        /// </summary>
        void GoToMain();

        /// <summary>
        /// Navigates user to Error screen
        /// </summary>
        /// <param name="title">The title of the error popup</param>
        /// <param name="message">The error message</param>
        /// <param name="recoveryAction">Action to be executed on confirmation of error popup</param>
        /// <param name="actionTitle">Action button content</param>
        void ShowError(string title, string message, Action recoveryAction, string actionTitle);

        /// <summary>
        /// Navigates user to Detailed Post Information screen
        /// </summary>
        /// <param name="postId">The post id for which detailed information to be shown</param>
        void GoToDetailedPost(int postId);
    }
}