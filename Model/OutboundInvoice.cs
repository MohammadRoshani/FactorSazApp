// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="OutboundInvoice.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WaybillApp.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class OutboundInvoice. This class cannot be inherited.
    /// </summary>
    public sealed class OutboundInvoice
    {
        /// <summary>
        /// Gets or sets the after fare.
        /// </summary>
        public int AfterFare { get; set; }

        /// <summary>
        /// Gets or sets the before fare.
        /// </summary>
        public int BeforeFare { get; set; }

        /// <summary>
        /// Gets or sets the bill way code.
        /// </summary>
        public int BillWayCode { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the destination city.
        /// </summary>
        public string DestinationCity { get; set; }

        /// <summary>
        /// Gets or sets the destination discharge.
        /// </summary>
        public string DestinationDischarge { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the receiver.
        /// </summary>
        /// <value>The full name of the receiver.</value>
        public string ReceiverFullName { get; set; }

        /// <summary>
        /// Gets or sets the receiver phone number.
        /// </summary>
        public string ReceiverPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the full name of the sender.
        /// </summary>
        /// <value>The full name of the sender.</value>
        public string SenderFullName { get; set; }

        /// <summary>
        /// Gets or sets the sender national identifier.
        /// </summary>
        public string SenderNationalId { get; set; }

        /// <summary>
        /// Gets or sets the sender phone number.
        /// </summary>
        public string SenderPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the urban fare.
        /// </summary>
        public int UrbanFare { get; set; }

        /// <summary>
        /// Gets or sets the wage.
        /// </summary>
        public int Wage { get; set; }

        /// <summary>
        /// Gets or sets the wares.
        /// </summary>
        public string Wares { get; set; }

        /// <summary>
        /// Gets the fare.
        /// </summary>
        public int Fare => this.UrbanFare + this.BeforeFare + this.AfterFare + this.Wage;
    }
}