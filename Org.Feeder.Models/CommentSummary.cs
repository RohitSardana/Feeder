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

namespace Org.Feeder.Models
{
    /// <summary>
    /// Class represents the comment summary.
    /// </summary>
    public class CommentSummary
    {
        /// <summary>
        /// Initializes the comment summary
        /// </summary>
        /// <param name="commenterName"></param>
        /// <param name="text"></param>
        public CommentSummary(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }

        /// <summary>
        /// Gets the Name of the commenter
        /// </summary>
        public string CommenterName { get; private set; }

        /// <summary>
        /// Gets the comment
        /// </summary>
        public string Text { get; private set; }
    }
}
