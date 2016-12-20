using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// Represents appliation shell to host any view model of type <see cref="Org.Feeder.App.ViewModels.IViewModel"/>
    /// </summary>
    public class AppShellViewModel : ViewModelBase, IContentHostViewModel
    {
        private IViewModel _content;

        /// <summary>
        /// Gets or sets the current ViewModel displayed on the application
        /// </summary>
        public IViewModel Content
        {
            get { return _content; }
            set
            {
                if (_content == value)
                    return;

                _content = value;
                OnPropertyChanged();
            }
        }
    }
}
