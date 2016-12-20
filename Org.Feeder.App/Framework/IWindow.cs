namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Interface for Host window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Gets or sets the data context of Host Window
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Shows the window hosting the data context
        /// </summary>
        void Show();
    }
}