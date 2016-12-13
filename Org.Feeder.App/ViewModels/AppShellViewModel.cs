using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    public class AppShellViewModel : ViewModelBase, IContentHostViewModel
    {
        private IViewModel _content;

        /// <summary>
        /// Current ViewModel displayed on the application
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
