using System.ComponentModel;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Marker interface for all view models, to enforce the implementation of PropertyChanged.
    /// </summary>
    /// <remarks>
    /// All view model should implement this.
    /// </remarks>
    public interface IViewModel : INotifyPropertyChanged { }
}