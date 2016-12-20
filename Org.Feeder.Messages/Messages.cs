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

namespace Org.Feeder.Common
{
    /// <summary>
    /// Represents messages displayed throughout the application.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Shows the message when application could not filter posts at Main Screen.
        /// </summary>
        public const string MainView_CouldNotFilterThePost = "Could not filter posts due to";

        /// <summary>
        /// Shows the message when application could not navigate user to Main Screen.
        /// </summary>
        public const string Navigate_CouldNotNavigateToMainScreen = "Could not navigate to Main screen due to";

        /// <summary>
        /// Shows the message when application could not navigate user to Detailed Post Screen.
        /// </summary>
        public const string Navigate_CouldNotNavigateToDetailedPostScreen = "Could not navigate to detailed post screen due to";

        /// <summary>
        /// Shows the message when application could not retrieve details of a post.
        /// </summary>
        public const string CouldNotFetchPostDetails = "Could not fetch post details";

        /// <summary>
        /// Shows the message when application could not retrieve details of post author.
        /// </summary>
        public const string CouldNotFetchUserDetails = "Could not fetch user details";

        /// <summary>
        /// Shows the message when application finds that post author details does not exists in database.
        /// </summary>
        public const string UserDoesNotExists = "User does not exist";

        /// <summary>
        /// Shows the message when application could not retrieve comments of a post.
        /// </summary>
        public const string CouldNotFetchComments = "Could not fetch comments of the post";

        /// <summary>
        /// Shows the message when comments of a post are not available.
        /// </summary>
        public const string CommentsNotAvailable = "Comments not available";

        /// <summary>
        /// Shows the text of the Comments Header
        /// </summary>
        public const string CommentsHeaderText = "Comments";

        /// <summary>
        /// Shows the message when application faces some unknown technical error.
        /// </summary>
        public const string TechnicalError = "Oops! Unknown technical error occured";

        /// <summary>
        /// Shows the text when information is not available
        /// </summary>
        public const string NotAvailable = "Not available";

        /// <summary>
        /// Show the "Error" text
        /// </summary>
        public const string Error = "Error";

        /// <summary>
        /// Show the "Go Back" text
        /// </summary>
        public const string GoBack = "Go Back";

        /// <summary>
        /// Show the "Retry" text
        /// </summary>
        public const string Retry = "Retry";

        /// <summary>
        /// Show the "Reload UI" text
        /// </summary>
        public const string ReloadUI = "Reload UI";
    }
}
