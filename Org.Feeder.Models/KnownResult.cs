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
using System;

namespace Org.Feeder.Models
{
    /// <summary>
    /// Represents the generic data and error message
    /// </summary>
    /// <remarks>Methods can use this class to wrap their return values and associated error message (if any)</remarks>
    /// <typeparam name="T">Any data type</typeparam>
    public class KnownResult<T>
    {
        #region Properties
        private string _errorMessage;


        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The errors.</value>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    HasError = true;
                }
                _errorMessage = value;
            }
        }
        /// <summary>
        /// Gets whether object has error.
        /// </summary>
        public bool HasError
        {
            get; private set;

        }

        #endregion
    }
}
