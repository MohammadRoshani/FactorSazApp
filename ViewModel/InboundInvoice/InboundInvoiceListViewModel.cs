// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="InboundInvoiceListViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.InboundInvoice
{
    using System;
    using System.Linq;

    using LinqKit;

    using Prism.Events;
    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Regions;

    using WaybillApp.Core;
    using WaybillApp.Core.Region;
    using WaybillApp.Model;
    using WaybillApp.View;

    /// <summary>
    /// Class InboundInvoiceListViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class InboundInvoiceListViewModel : ListBaseViewModel<InboundInvoice>
    {
        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        private string driverFilter;

        private string ownerNameFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundInvoiceListViewModel" /> class.
        /// </summary>
        /// <param name="containerExtension">The container extension.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public InboundInvoiceListViewModel(
            IContainerExtension containerExtension,
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IUnitOfWorkExtended unitOfWorkExtended)
            : base(containerExtension, regionManager, eventAggregator)
        {
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.OrderSelector = invoices => invoices.OrderByDescending(x => x.Id);
            this.ClearFilter();
            this.EventAggregator.GetEvent<PubSubEvents.AddInboundInvoice>().Subscribe(invoice => this.Items.Insert(0, invoice));
            this.EventAggregator.GetEvent<PubSubEvents.UpdateInboundInvoice>().Subscribe(
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
        /// Gets or sets the driver filter.
        /// </summary>
        public string DriverFilter
        {
            get => this.driverFilter;
            set => this.SetProperty(ref this.driverFilter, value);
        }

        /// <summary>
        /// Gets or sets the owner name filter.
        /// </summary>
        public string OwnerNameFilter
        {
            get => this.ownerNameFilter;
            set => this.SetProperty(ref this.ownerNameFilter, value);
        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        protected override async void UpdatePage()
        {
            this.ItemsCount = this.unitOfWorkExtended.InboundInvoices.Count();

            this.Items.Clear();
            var inboundInvoices =
                this.Predicate == null
                    ? await this.unitOfWorkExtended.InboundInvoices.GetPagedListAsync(null, this.OrderSelector, null, (int)this.SelectedPageIndex, this.PageSize)
                    : await this.unitOfWorkExtended.InboundInvoices.GetPagedListAsync(this.Predicate, this.OrderSelector, null, (int)this.SelectedPageIndex, this.PageSize);
            foreach (var inboundInvoice in inboundInvoices.Items)
            {
                this.Items.Add(inboundInvoice);
            }
        }

        /// <summary>
        /// Deletes the specified inbound invoice.
        /// </summary>
        /// <param name="inboundInvoice">The inbound invoice.</param>
        protected override void Delete(InboundInvoice inboundInvoice)
        {
            this.unitOfWorkExtended.InboundInvoices.Delete(inboundInvoice);
            this.unitOfWorkExtended.SaveChangesAsync();

            this.Items.Remove(inboundInvoice);
            this.EventAggregator.GetEvent<PubSubEvents.DeleteInboundInvoice>().Publish(inboundInvoice);
        }

        /// <summary>
        /// Edits the specified inbound invoice.
        /// </summary>
        /// <param name="inboundInvoice">The inbound invoice.</param>
        protected override void Edit(InboundInvoice inboundInvoice)
        {
            if (inboundInvoice != null)
            {
                this.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(InboundInvoiceView), new NavigationParameters() { { "IInvoice", inboundInvoice } });
            }
        }

        /// <summary>
        /// Clears the filter.
        /// </summary>
        protected override sealed void ClearFilter()
        {
            this.DriverFilter = null;
            this.OwnerNameFilter = null;
            base.ClearFilter();
        }

        /// <summary>
        /// Filters this instance.
        /// </summary>
        protected override void Filter()
        {
            this.Predicate = PredicateBuilder.New<InboundInvoice>(true);

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

            if (!string.IsNullOrEmpty(this.DriverFilter))
            {
                this.Predicate = this.Predicate.And(x => x.DriverName.Contains(this.DriverFilter));
            }

            if (!string.IsNullOrEmpty(this.OwnerNameFilter))
            {
                this.Predicate = this.Predicate.And(x => x.OwnerName.Contains(this.OwnerNameFilter));
            }

            if (!string.IsNullOrEmpty(this.LocationFilter))
            {
                this.Predicate = this.Predicate.And(x => x.OriginCityName.Contains(this.LocationFilter));
            }

            this.UpdatePage();
        }
    }
}