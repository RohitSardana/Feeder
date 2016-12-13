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
    }
}
