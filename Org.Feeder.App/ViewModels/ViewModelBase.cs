using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Implementation of<see cref="IViewModel"/> and base class for all view model classes
    /// </summary>
    public abstract class ViewModelBase : IViewModel
    {
        /// <summary>
        /// Occures when any of the property of view model changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event handler of propertyChanged event
        /// </summary>
        /// <remarks>This is a virtual method, child classes can override it.</remarks>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}