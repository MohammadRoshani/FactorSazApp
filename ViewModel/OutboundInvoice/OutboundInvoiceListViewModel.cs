// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="OutboundInvoiceListViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.OutboundInvoice
{
    using System;
    using System.Linq;
    using System.Windows.Controls;

    using LinqKit;

    using Prism.Events;
    using Prism.Ioc;
    using Prism.Regions;

    using WaybillApp.Common;
    using WaybillApp.Core;
    using WaybillApp.Core.Region;
    using WaybillApp.Model;
    using WaybillApp.View;

    /// <summary>
    /// Class OutboundInvoiceListViewModel.
    /// Implements the <see cref="ListBaseViewModel{OutboundInvoice}" />
    /// </summary>
    /// <seealso cref="ListBaseViewModel{OutboundInvoice}" />
    public class OutboundInvoiceListViewModel : ListBaseViewModel<OutboundInvoice>
    {
        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        private string senderFilter;

        private string receiverFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoiceListViewModel" /> class.
        /// </summary>
        /// <param name="containerExtension">The container extension.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public OutboundInvoiceListViewModel(
            IContainerExtension containerExtension,
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IUnitOfWorkExtended unitOfWorkExtended)
            : base(containerExtension, regionManager, eventAggregator)
        {
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.OrderSelector = invoices => invoices.OrderByDescending(x => x.Id);
            this.ClearFilter();

            this.EventAggregator.GetEvent<PubSubEvents.AddOutboundInvoice>().Subscribe(invoice => this.Items.Insert(0, invoice));
            this.EventAggregator.GetEvent<PubSubEvents.UpdateOutboundInvoice>().Subscribe(
                invoice =>
                    {
                        var itemIndex = this.Items.IndexOf(this.Items.FirstOrDefault(x => x.Id == invoice.Id));
                        if (itemIndex != -1)
                        {
                            this.Items.RemoveAt(itemIndex);
                            this.Items.Insert(itemIndex, invoice);
                        }
                        else
                        {
                            this.Items.Insert(0, invoice);
                        }
                    });
        }

        /// <summary>
        /// Gets or sets the sender filter.
        /// </summary>
        public string SenderFilter
        {
            get => this.senderFilter;
            set => this.SetProperty(ref this.senderFilter, value);
        }

        /// <summary>
        /// Gets or sets the receiver filter.
        /// </summary>
        public string ReceiverFilter
        {
            get => this.receiverFilter;
            set => this.SetProperty(ref this.receiverFilter, value);
        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        protected override async void UpdatePage()
        {
            this.ItemsCount = this.unitOfWorkExtended.OutboundInvoices.Count();

            this.Items.Clear();
            var outboundInvoices =
                this.Predicate == null
                    ? await this.unitOfWorkExtended.OutboundInvoices.GetPagedListAsync(null, this.OrderSelector, null, (int)this.SelectedPageIndex, this.PageSize)
                    : await this.unitOfWorkExtended.OutboundInvoices.GetPagedListAsync(this.Predicate, this.OrderSelector, null, (int)this.SelectedPageIndex, this.PageSize);
            foreach (var outboundInvoice in outboundInvoices.Items)
            {
                this.Items.Add(outboundInvoice);
            }
        }

        /// <summary>
        /// Deletes the specified outbound invoice.
        /// </summary>
        /// <param name="outboundInvoice">The outbound invoice.</param>
        protected override async void Delete(OutboundInvoice outboundInvoice)
        {
            this.unitOfWorkExtended.OutboundInvoices.Delete(outboundInvoice);
            await this.unitOfWorkExtended.SaveChangesAsync();

            this.Items.Remove(outboundInvoice);
            this.EventAggregator.GetEvent<PubSubEvents.DeleteOutboundInvoice>().Publish(outboundInvoice);
        }

        /// <summary>
        /// Edits the specified outbound invoice.
        /// </summary>
        /// <param name="outboundInvoice">The outbound invoice.</param>
        protected override void Edit(OutboundInvoice outboundInvoice)
        {
            if (outboundInvoice != null)
            {
                this.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(OutboundInvoiceView), new NavigationParameters() { { "OInvoice", outboundInvoice } });
            }
        }

        /// <summary>
        /// Prints the specified outbound invoice.
        /// </summary>
        /// <param name="outboundInvoice">The outbound invoice.</param>
        protected override void Print(OutboundInvoice outboundInvoice)
        {
            if (this.ContainerExtension.Resolve<OutboundInvoicePrintView>() is OutboundInvoicePrintView invoicePrintView)
            {
                this.EventAggregator.GetEvent<PubSubEvents.SelectOutboundInvoice>().Publish(outboundInvoice);
                PrintHelper.ShowPrintPreview(PrintHelper.GetFixedDocument3(invoicePrintView, new PrintDialog()));
            }
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="outboundInvoice">The outbound invoice.</param>
        protected override void SendSms(OutboundInvoice outboundInvoice)
        {
            this.EventAggregator.GetEvent<PubSubEvents.SelectOutboundInvoice>().Publish(outboundInvoice);
        }

        /// <summary>
        /// Clears the filter.
        /// </summary>
        protected override sealed void ClearFilter()
        {
            this.SenderFilter = null;
            this.ReceiverFilter = null;
            base.ClearFilter();
        }

        /// <summary>
        /// Filters this instance.
        /// </summary>
        protected override void Filter()
        {
            this.Predicate = PredicateBuilder.New<OutboundInvoice>(true);

            if (this.FromDateFilter.HasValue && this.FromDateFilter != DateTime.MinValue)
            {
                this.Predicate = this.Predicate.And(x => x.Date >= this.FromDateFilter);
            }

            if (this.ToDateFilter.HasValue && this.ToDateFilter != DateTime.MinValue)
            {
                this.Predicate = this.Predicate.And(x => x.Date <= this.ToDateFilter);
            }

            if (this.BillCodeFilter.HasValue)
            {
                this.Predicate = this.Predicate.And(x => x.BillWayCode == this.BillCodeFilter);
            }

            if (!string.IsNullOrEmpty(this.SenderFilter))
            {
                this.Predicate = this.Predicate.And(x => x.SenderFullName.Contains(this.SenderFilter));
            }

            if (!string.IsNullOrEmpty(this.ReceiverFilter))
            {
                this.Predicate = this.Predicate.And(x => x.ReceiverFullName.Contains(this.ReceiverFilter));
            }

            if (!string.IsNullOrEmpty(this.LocationFilter))
            {
                this.Predicate = this.Predicate.And(x => x.DestinationCity.Contains(this.LocationFilter) || x.DestinationDischarge.Contains(this.LocationFilter));
            }

            this.UpdatePage();
        }
    }
}