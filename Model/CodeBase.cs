// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-07-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="CodeBase.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class CodeBase.
    /// </summary>
    public class CodeBase
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Key]
        public string Code { get; set; }
    }
}
