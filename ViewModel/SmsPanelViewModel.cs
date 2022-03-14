// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="SmsPanelViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel
{
    using System;
    using System.Linq;

    using Prism.Mvvm;

    using WaybillApp.ViewModel.OutboundInvoice;

    /// <summary>
    /// Class SmsPanelViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class SmsPanelViewModel : BindableBase
    {
        /// <summary>
        /// The outbound invoice view model
        /// </summary>
        private OutboundInvoiceViewModel outboundInvoiceViewModel;

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>The content of the message.</value>
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the outbound invoice view model.
        /// </summary>
        public OutboundInvoiceViewModel OutboundInvoiceViewModel
        {
            get => this.outboundInvoiceViewModel;
            set
            {
                this.SetProperty(ref this.outboundInvoiceViewModel, value);
                this.MessageContent = "بارنامه به شماره: " + this.outboundInvoiceViewModel.BillWayCode
                                                           + Environment.NewLine + "به نام: "
                                                           + this.outboundInvoiceViewModel.ReceiverFullName
                                                           + Environment.NewLine + "شامل موارد:" + Environment.NewLine
                                                           + string.Join(
                                                               Environment.NewLine,
                                                               value.Wares.Select(x => x.Name)) + Environment.NewLine
                                                           + "به مقصد: "
                                                           + this.outboundInvoiceViewModel.DestinationCity + "-"
                                                           + this.outboundInvoiceViewModel.DestinationDischarge
                                                           + " ارسال شد." + Environment.NewLine
                                                           + "خدمات باربری جهان نما";
            }
        }
    }
}