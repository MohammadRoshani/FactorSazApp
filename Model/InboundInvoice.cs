// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="InboundInvoice.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WaybillApp.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class InboundInvoice.
    /// </summary>
    public class InboundInvoice
    {
        /// <summary>
        /// Gets or sets the bill way code.
        /// </summary>
        public int BillWayCode { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the driver code.
        /// </summary>
        public string DriverCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        /// <value>The name of the driver.</value>
        public string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        public string Fare { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the origin city code.
        /// </summary>
        public string OriginCityCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the origin city.
        /// </summary>
        /// <value>The name of the origin city.</value>
        public string OriginCityName { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        /// <value>The name of the owner.</value>
        public string OwnerName { get; set; }

        /// <summary>
        /// Gets or sets the owner phone.
        /// </summary>
        public string OwnerPhone { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the name of the ware.
        /// </summary>
        /// <value>The name of the ware.</value>
        public string WareName { get; set; }
    }
}