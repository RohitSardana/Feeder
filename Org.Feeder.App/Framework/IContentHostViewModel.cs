using Org.Feeder.App.ViewModels;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Interface for content viewModel
    /// </summary>
    public interface IContentHostViewModel : IViewModel
    {
        /// <summary>
        /// Holds the reference of IViewModel
        /// </summary>
        IViewModel Content { get; set; }
    }
}