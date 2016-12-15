/***************************************************************************************************
*
*  This source forms a part of the Software System and is a copyright of
*  Global Logic India Private Ltd. (GLIPL)

*  All rights are reserved with GLIPT. No part of this work may be reproduced, stored,
*  adopted or transmitted in any form or by any means including but not limiting to
*  electronic, mechanical, photographic, graphic, optic recording or otherwise,
*  translated in any language or computer language, without the prior written permission
*  of

*  Global Logic India Private Limited
*  Tower A, Oxygen Park, 
*  Plot No.7, Sector-144, 
*  Noida-Greater Noida Expressway, 
*  Noida, Uttar Pradesh 201304

*  Copyright 2016-2017 Global Logic India Private Limited
*
* -------------------------------------------------------------------------------------------------
*  Author   : Rohit Sardana   
* -------------------------------------------------------------------------------------------------
* 
**************************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;
using System.Threading.Tasks;
using System;
using Org.Feeder.App.Services;

namespace Org.Feeder.App.ViewModels
{
    public class DetailedPostViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IDbService _dbService;
        private readonly int _postId;
        private bool _isBusy = false;
        public event Action OnInitialized;
        private string _title = String.Empty;
        private string _body = String.Empty;
        private string _author = String.Empty;
        private string _imageUrl = String.Empty;
        private string _commentHeaderText = String.Empty;
        private List<CommentSummary> _comments = new List<CommentSummary>();

        /// <summary>
        /// Initializes the view model about post's detailed information
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="dbService"></param>
        /// <param name="postId"></param>
        public DetailedPostViewModel(INavigator navigator, IDbService dbService, int postId)
        {
            _navigator = navigator;
            _dbService = dbService;
            _postId = postId;
            Task.Run(() => PopulateViewModel());
            GoBackCommand = new ActionCommand(GoBack);
        }
        /// <summary>
        /// Gets the title of the post
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        /// <summary>
        /// Gets the body of the post
        /// </summary>
        public string Body
        {
            get { return _body; }
            private set
            {
                _body = value;
                OnPropertyChanged("Body");
            }
        }
        /// <summary>
        /// Gets the author of the post
        /// </summary>
        public string Author
        {
            get { return _author; }
            private set
            {
                _author = value;
                OnPropertyChanged("Author");
            }
        }
        /// <summary>
        /// Gets the image url of the post
        /// </summary>
        public string ImageUrl
        {
            get { return _imageUrl; }
            private set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
        /// <summary>
        /// Gets the Comment Header Text
        /// </summary>
        public string CommentHeaderText
        {
            get { return _commentHeaderText; }
            private set
            {
                _commentHeaderText = value;
                OnPropertyChanged("CommentHeaderText");
            }
        }
        /// <summary>
        /// Gets comments of the post
        /// </summary>
        public List<CommentSummary> Comments
        {
            get { return _comments; }
            private set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        /// <summary>
        /// A command to move back to Main screen
        /// </summary>
        public ActionCommand GoBackCommand { get; private set; }

        private void PopulateViewModel()
        {
            IsBusy = true;
            List<string> errors = new List<string>();
            try
            {
                bool isPostInfoAvailable = false;
                bool isUserInfoAvailable = false;
                bool areCommentsAvailable = false;
                string title = "Not available.";
                string body = "Not available.";
                string author = "Not available.";
                string imageUrl = String.Empty;
                List<CommentSummary> comments = new List<CommentSummary>();
                Parallel.Invoke(
                    () =>
                    {
                        KnownResult<FeederDb.Post> postResult = _dbService.GetPostById(_postId);
                        if (postResult == null)
                        {
                            errors.Add("Could not fetch post details.");
                        }
                        else if (postResult.HasError)
                        {
                            errors.Add(postResult.ErrorMessage);
                        }
                        else
                        {
                            isPostInfoAvailable = true;
                            FeederDb.Post post = postResult.Data;
                            title = post.Title;
                            body = post.Body;
                            KnownResult<FeederDb.User> userResult = _dbService.GetUserById(post.UserId);
                            if (userResult == null)
                            {
                                errors.Add("Could not fetch user details.");
                            }
                            else if (userResult.HasError)
                            {
                                errors.Add(userResult.ErrorMessage);
                            }
                            else
                            {
                                isUserInfoAvailable = true;
                                FeederDb.User user = userResult.Data;
                                author = user.Name;
                                imageUrl = user.ImageUrl;
                            }
                        }
                    },
                    () =>
                    {
                        KnownResult<IList<CommentSummary>> commentsResult = _dbService.GetCommentSummaryByPostId(_postId);
                        if (commentsResult == null)
                        {
                            errors.Add("Could not fetch comments of the post.");
                        }
                        else if (commentsResult.HasError)
                        {
                            errors.Add(commentsResult.ErrorMessage);
                        }
                        else
                        {
                            areCommentsAvailable = true;
                            comments = commentsResult.Data.ToList();
                        }
                    }
                    );

                /*Display the information on screen only if post information is available.
                If post information is not available, 'navigate user to error screen.
                If post information is available, whereas either user info or comments info are not available, Continue to display data.
                For not available user or comments information, UI will show text as 'Not available'
                  */
                if (isPostInfoAvailable)
                {
                    Title = title;
                    Body = body;
                    if (isUserInfoAvailable)
                    {
                        Author = author;
                        ImageUrl = imageUrl;
                    }
                    if (areCommentsAvailable)
                    {
                        CommentHeaderText = "Comments";
                        Comments = comments;
                    }
                    else
                    {
                        CommentHeaderText = "Comments not available";
                    }
                }
                else
                {
                    ShowErrorMessage(String.Join(String.Format("{0}", System.Environment.NewLine), errors.Distinct()), "Go Back");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Go Back");
            }
            finally
            {
                IsBusy = false;
                //In case any observer is waiting for data to be initialized, firing the OnInitialied event to notify 
                if (OnInitialized != null)
                    OnInitialized.BeginInvoke(null, null);
            }
        }
        /// <summary>
        /// Navigates the user back to Main screen.
        /// </summary>
        public void GoBack()
        {
            try
            {
                _navigator.GoToMain();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(String.Format("Could not navigate to Post Summary UI due to {0}", ex.Message), "Retry");
            }
        }

        private void ShowErrorMessage(string errorMessage, string actionTitle)
        {
            _navigator.ShowError("Error", errorMessage, () => GoBack(), actionTitle);
        }
    }
}
