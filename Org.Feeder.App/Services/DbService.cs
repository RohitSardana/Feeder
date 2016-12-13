﻿/***************************************************************************************************
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
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Feeder.App.Services
{
    /// <summary>
    /// A service to fetch the records from database.
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
        /// <returns></returns>
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
        /// <param name="postId"></param>
        /// <returns></returns>
        public KnownResult<IList<CommentSummary>> GetCommentSummaryByPostId(int postId)
        {
            KnownResult<IList<CommentSummary>> commentsResult = new KnownResult<IList<CommentSummary>>();
            IList<CommentSummary> comments = new List<CommentSummary>();
            String error = String.Empty;
            try
            {
                comments = _database.GetComments(postId).Select(c => new CommentSummary(c.CommenterName, c.Text)).ToList();
            }
            catch (Exception ex)
            {
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
        /// <param name="userId"></param>
        /// <returns></returns>
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
                        error = String.Format("User with user id {0} does not exists.", userId);
                    }
                }
                else
                {
                    error = "Oops! Could not fetch users from database.";
                }
            }
            catch (Exception ex)
            {
                error = String.Format("Oops! Could not fetch users from database due to {0}", ex.Message);
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
        /// <param name="postId"></param>
        /// <returns></returns>
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