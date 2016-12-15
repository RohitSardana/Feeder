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
using Org.Feeder.App.Models;
using System.Collections.Generic;

namespace Org.Feeder.App.Services
{

    /// <summary>
    /// Declars the method to fetch records from database.
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Gets post summaries from database.
        /// </summary>
        /// <returns></returns>
        KnownResult<IEnumerable<PostSummary>> GetPostSummaries();

        /// <summary>
        /// Gets comments by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        KnownResult<IList<CommentSummary>> GetCommentSummaryByPostId(int postId);

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        KnownResult<FeederDb.User> GetUserById(int userId);

        /// <summary>
        /// Gets post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        KnownResult<FeederDb.Post> GetPostById(int postId);
    }
}
