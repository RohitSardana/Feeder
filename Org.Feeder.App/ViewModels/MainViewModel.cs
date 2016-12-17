using System.Collections.Generic;
using System.Linq;
using Org.Feeder.App.Framework;
using Org.Feeder.Models;
using Org.Feeder.Services;
using System;
using System.Threading.Tasks;
using Org.Feeder.Common;

namespace Org.Feeder.App.ViewModels
{
    /// <summary>
    /// A class to store data related to Main screen.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IDbService _dbService;
        private List<Models.PostSummary> _posts;
        private List<Models.PostSummary> _initialPosts;
        public event Action OnInitialized;
        private bool _isBusy = false;

        /// <summary>
        /// Initializes the data related to Main screen.
        /// It also fetches the posts from database.
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="dbService"></param>
        public MainViewModel(INavigator navigator, IDbService dbService)
        {
            _navigator = navigator;
            _dbService = dbService;
            Posts = new List<PostSummary>();
            InitializeData();
            FilterCommand = new ParametrizedCommand<string>(Filter);
            SelectCommand = new ParametrizedCommand<PostSummary>(PostSelected);
        }

        /// <summary>
        /// Gets the list of posts
        /// </summary>
        public List<PostSummary> Posts
        {
            get { return _posts; }
            private set
            {
                _posts = value;
                OnPropertyChanged("Posts");
            }
        }

        /// <summary>
        /// Gets whether application is busy or not.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// A command to execute the method to filter posts.
        /// </summary>
        public ParametrizedCommand<string> FilterCommand { get; private set; }

        /// <summary>
        /// A command to execute the method to select a specific post
        /// </summary>
        public ParametrizedCommand<PostSummary> SelectCommand { get; private set; }

        private List<PostSummary> InitializeData()
        {
            IsBusy = true;
            List<PostSummary> posts = new List<PostSummary>();
            try
            {
                Task<KnownResult<IEnumerable<PostSummary>>> task = Task<KnownResult<IEnumerable<PostSummary>>>.Run(() => _dbService.GetPostSummaries());
                List<string> errorsList = new List<string>();
                task.ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        Exception ex = t.Exception;
                        while (ex is AggregateException && ex.InnerException != null)
                            ex = ex.InnerException;
                        errorsList.Add(ex.Message);
                    }
                    else if (t.IsCompleted)
                    {
                        var postSummariesResult = t.Result;
                        if (postSummariesResult == null)
                        {
                            errorsList.Add(Messages.TechnicalError);
                        }
                        else if (postSummariesResult.HasError || postSummariesResult.Data == null)
                        {
                            string errorMessage = String.Empty;
                            if (String.IsNullOrWhiteSpace(postSummariesResult.ErrorMessage))
                            {
                                errorMessage = Messages.TechnicalError;
                            }
                            else
                            {
                                errorMessage = postSummariesResult.ErrorMessage;
                            }
                            errorsList.Add(errorMessage);
                        }
                        else
                        {
                            _initialPosts = postSummariesResult.Data.ToList();
                            Posts = _initialPosts.ToList();
                        }
                    }
                    if (errorsList != null && errorsList.Count > 0)
                    {
                        _navigator.ShowError(Messages.Error, String.Join(String.Format("{0}", System.Environment.NewLine), errorsList), () => _navigator.GoToMain(), Messages.Retry);
                    }
                    IsBusy = false;
                    if (OnInitialized != null)
                    {
                        OnInitialized.BeginInvoke(null, null);
                    }
                });
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _navigator.ShowError(Messages.Error, ex.Message, () => _navigator.GoToMain(), Messages.Retry);
            }
            return posts;
        }

        private void Filter(string filter)
        {
            try
            {
                Posts.Clear();
                var filteredPosts =
                        _initialPosts.Where(x => x.Title.ToLower().Contains(filter.ToLower()));

                Posts = filteredPosts.ToList();
            }
            catch (Exception ex)
            {
                _navigator.ShowError(Messages.Error, String.Format("{0} {1}.", Messages.MainView_CouldNotFilterThePost, ex.Message), () => _navigator.GoToMain(), Messages.ReloadUI);
            }
        }

        private void PostSelected(PostSummary postSummary)
        {
            try
            {
                _navigator.GoToDetailedPost(postSummary.PostId);
            }
            catch (Exception ex)
            {
                _navigator.ShowError(Messages.Error, String.Format("{0} {1}", Messages.Navigate_CouldNotNavigateToDetailedPostScreen, ex.Message), () => _navigator.GoToMain(), Messages.ReloadUI);
            }
        }
    }
}
