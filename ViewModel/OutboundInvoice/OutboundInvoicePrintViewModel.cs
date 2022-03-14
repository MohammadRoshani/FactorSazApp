// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-09-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="OutboundInvoicePrintViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.OutboundInvoice
{
    using Prism.Events;

    using WaybillApp.Core;

    /// <summary>
    /// Class OutboundInvoicePrintViewModel.
    /// Implements the <see cref="OutboundInvoiceBaseViewModel" />
    /// </summary>
    /// <seealso cref="OutboundInvoiceBaseViewModel" />
    public class OutboundInvoicePrintViewModel : OutboundInvoiceBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoicePrintViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public OutboundInvoicePrintViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<PubSubEvents.SelectOutboundInvoice>().Subscribe(invoice => this.Model = invoice);
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        public string Rules => "1- مدت اعتبار این برگه یک هفته میباشد.";

        /// <summary>
        /// Gets the bar code string.
        /// </summary>
        public string BarCodeString =>
            new PersianDateTime(this.OutboundDate).ToString("yyyyMMdd")
            + new PersianDateTime(this.OutboundTime).ToString("HHmm") + this.BillWayCode;

        protected override void ModelUpdated()
        {
            base.ModelUpdated();
            this.RaisePropertyChanged(nameof(this.BarCodeString));
        }
    }
}
