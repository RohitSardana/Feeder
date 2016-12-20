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
using Org.Feeder.Models;
using Org.Feeder.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Org.Feeder.Services
{
    /// <summary>
    /// Implements <see cref="Org.Feeder.Services.IDbService"/> to provide methods to fetch the records from database.
    /// </summary>
    public class DbService : IDbService
    {
        private readonly FeederDb.Database _database;
        private IEnumerable<FeederDb.User> _users;

        /// <summary>
        /// Initializes the service class with database to be used.
        /// </summary>
        public DbService()
        {
            _database = new FeederDb.Database(ConnectionStrings.UnreliableConnection);
        }

        /// <summary>
        /// Gets all posts summary from database.
        /// </summary>
        /// <returns><see cref="Org.Feeder.Models.KnownResult{T}"/> Where T is IEnumerable of <see cref="Org.Feeder.Models.PostSummary"/></returns>
        public KnownResult<IEnumerable<PostSummary>> GetPostSummaries()
        {
            KnownResult<IEnumerable<PostSummary>> postSummariesResult = new KnownResult<IEnumerable<PostSummary>>();
            IEnumerable<PostSummary> postSummaries = null;
            String error = String.Empty;
            try
            {
                postSummaries = _database.GetPostSummaries()
                       .Select(p => new PostSummary(p.Id, p.Title));
            }
            catch (Exception ex)
            {
                postSummaries = null;
                error = ex.Message;
            }
            finally
            {
                postSummariesResult.Data = postSummaries;
                postSummariesResult.ErrorMessage = error;
            }
            return postSummariesResult;
        }

        /// <summary>
        /// Gets the comment summary by post id
        /// </summary>
        /// <param name="postId">The post id</param>
        /// <returns><see cref="Org.Feeder.Models.KnownResult{T}"/> Where T is IList of <see cref="Org.Feeder.Models.CommentSummary"/></returns>
        public KnownResult<IList<CommentSummary>> GetCommentSummaryByPostId(int postId)
        {
            KnownResult<IList<CommentSummary>> commentsResult = new KnownResult<IList<CommentSummary>>();
            IList<CommentSummary> comments = null;
            String error = String.Empty;
            try
            {
                comments = _database.GetComments(postId).Select(c => new CommentSummary(c.CommenterName, c.Text)).ToList();
            }
            catch (Exception ex)
            {
                comments = null;
                error = ex.Message;
            }
            finally
            {
                commentsResult.Data = comments;
                commentsResult.ErrorMessage = error;
            }
            return commentsResult;
        }

        /// <summary>
        /// Gets the user by user id
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns><see cref="Org.Feeder.Models.KnownResult{T}"/> Where T is List of <see cref="Org.Feeder.FeederDb.User"/></returns>
        public KnownResult<FeederDb.User> GetUserById(int userId)
        {
            KnownResult<FeederDb.User> userResult = new KnownResult<FeederDb.User>();
            FeederDb.User user = null;
            String error = String.Empty;
            try
            {
                if (_users == null)
                {
                    var usersResult = GetUsers();
                    if (usersResult != null && !usersResult.HasError)
                    {
                        _users = usersResult.Data;
                    }
                    else
                        _users = null;
                }
                if (_users != null)
                {
                    user = _users.Where(u => u.UserId == userId).FirstOrDefault();
                    if (user == null)
                    {
                        error = String.Format("{0}: {1}",Messages.UserDoesNotExists, userId);
                    }
                }
                else
                {
                    error = String.Format("{0}.",Messages.CouldNotFetchUserDetails);
                }
            }
            catch (Exception ex)
            {
                user = null;
                error = String.Format("{0}: {1}", Messages.CouldNotFetchUserDetails, ex.Message);
            }
            finally
            {
                userResult.Data = user;
                userResult.ErrorMessage = error;
            }
            return userResult;
        }

        /// <summary>
        /// Gets the post by post id
        /// </summary>
        /// <param name="postId">The post id</param>
        /// <returns><see cref="Org.Feeder.Models.KnownResult{T}"/> Where T is of type <see cref="Org.Feeder.FeederDb.Post"/></returns>
        public KnownResult<FeederDb.Post> GetPostById(int postId)
        {
            KnownResult<FeederDb.Post> postResult = new KnownResult<FeederDb.Post>();
            FeederDb.Post post = null;
            String error = String.Empty;
            try
            {
                post = _database.GetPost(postId);
            }
            catch (Exception ex)
            {
                post = null;
                error = ex.Message;
            }
            finally
            {
                postResult.Data = post;
                postResult.ErrorMessage = error;
            }
            return postResult;
        }

        private KnownResult<IEnumerable<FeederDb.User>> GetUsers()
        {
            KnownResult<IEnumerable<FeederDb.User>> userResult = new KnownResult<IEnumerable<FeederDb.User>>();
            IEnumerable<FeederDb.User> users = null;
            String error = String.Empty;
            try
            {
                users = _database.GetUsers();
            }
            catch (Exception ex)
            {
                users = null;
                error = ex.Message;
            }
            finally
            {
                userResult.Data = users;
                userResult.ErrorMessage = error;
            }
            return userResult;
        }
    }
}
