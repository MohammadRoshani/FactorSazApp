// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-11-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-11-2020
// ***********************************************************************
// <copyright file="ListBaseViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Regions;

    /// <summary>
    /// Class ListBaseViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <seealso cref="BindableBase" />
    public class ListBaseViewModel<T> : BindableBase
    {
        private DateTime? toDateFilter;

        private DateTime? fromDateFilter;

        private int? billCodeFilter;

        private string locationFilter;

        private int pageSize = 10;

        private long itemsCount;

        private long selectedPageIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBaseViewModel{T}"/> class.
        /// </summary>
        /// <param name="containerExtension">The container extension.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected ListBaseViewModel(IContainerExtension containerExtension, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.ContainerExtension = containerExtension;
            this.RegionManager = regionManager;
            this.EventAggregator = eventAggregator;
            this.FilterCommand = new DelegateCommand(this.Filter);
            this.ClearFilterCommand = new DelegateCommand(this.ClearFilter);
            this.DeleteCommand = new DelegateCommand<T>(this.Delete);
            this.EditCommand = new DelegateCommand<T>(this.Edit);
            this.PrintCommand = new DelegateCommand<T>(this.Print);
            this.SendSmsCommand = new DelegateCommand<T>(this.SendSms);
            this.UpdatePageCommand = new DelegateCommand(this.UpdatePage);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        public DelegateCommand<T> DeleteCommand { get; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public DelegateCommand<T> EditCommand { get; }

        /// <summary>
        /// Gets the print command.
        /// </summary>
        public DelegateCommand<T> PrintCommand { get; }

        /// <summary>
        /// Gets the send SMS command.
        /// </summary>
        public DelegateCommand<T> SendSmsCommand { get; }

        /// <summary>
        /// Gets the filter command.
        /// </summary>
        public DelegateCommand FilterCommand { get; }

        /// <summary>
        /// Gets the clear filter command.
        /// </summary>
        public DelegateCommand ClearFilterCommand { get; }

        /// <summary>
        /// Gets the update page command.
        /// </summary>
        public DelegateCommand UpdatePageCommand { get; }

        /// <summary>
        /// Gets or sets the predicate.
        /// </summary>
        public Expression<Func<T, bool>> Predicate { get; set; }

        /// <summary>
        /// Gets or sets the order selector.
        /// </summary>
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderSelector { get; set; }

        /// <summary>
        /// Gets or sets the bill code filter.
        /// </summary>
        public int? BillCodeFilter
        {
            get => this.billCodeFilter;
            set => this.SetProperty(ref this.billCodeFilter, value);
        }

        /// <summary>
        /// Gets or sets to date filter.
        /// </summary>
        public DateTime? ToDateFilter
        {
            get => this.toDateFilter;
            set => this.SetProperty(ref this.toDateFilter, value);
        }

        /// <summary>
        /// Gets or sets from date filter.
        /// </summary>
        public DateTime? FromDateFilter
        {
            get => this.fromDateFilter;
            set => this.SetProperty(ref this.fromDateFilter, value);
        }

        /// <summary>
        /// Gets or sets the location filter.
        /// </summary>
        public string LocationFilter
        {
            get => this.locationFilter;
            set => this.SetProperty(ref this.locationFilter, value);
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get => this.pageSize;
            set => this.SetProperty(ref this.pageSize, value);
        }

        /// <summary>
        /// Gets or sets the items count.
        /// </summary>
        public long ItemsCount
        {
            get => this.itemsCount;
            set => this.SetProperty(ref this.itemsCount, value);
        }

        /// <summary>
        /// Gets or sets the index of the selected page.
        /// </summary>
        public long SelectedPageIndex
        {
            get => this.selectedPageIndex;
            set => this.SetProperty(ref this.selectedPageIndex, value);
        }

        /// <summary>
        /// Gets the container extension.
        /// </summary>
        protected IContainerExtension ContainerExtension { get; }

        /// <summary>
        /// Gets the region manager.
        /// </summary>
        protected IRegionManager RegionManager { get; }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Updates the page.
        /// </summary>=
        protected virtual void UpdatePage()
        {
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void Delete(T item)
        {
        }

        /// <summary>
        /// Edits the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void Edit(T item)
        {
        }

        /// <summary>
        /// Prints the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void Print(T item)
        {
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void SendSms(T item)
        {
        }

        /// <summary>
        /// Clears the filter.
        /// </summary>
        protected virtual void ClearFilter()
        {
            this.FromDateFilter = null;
            this.ToDateFilter = null;
            this.BillCodeFilter = null;
            this.Predicate = null;

            this.UpdatePage();
        }

        /// <summary>
        /// Filters this instance.
        /// </summary>
        protected virtual void Filter()
        {
        }
    }
}
