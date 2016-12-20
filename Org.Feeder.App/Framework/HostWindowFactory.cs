using Org.Feeder.App.Views;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Provides members to create host window
    /// </summary>
    public class HostWindowFactory
    {
        /// <summary>
        /// Creates the window to host view model
        /// </summary>
        /// <param name="viewModel">The data context for the host window</param>
        /// <returns>Host window of type <see cref="IWindow"></see> </returns>
        public virtual IWindow CreateHostWindow(IContentHostViewModel viewModel)
        {
            return new AppWindow { DataContext = viewModel };
        }
    }
}