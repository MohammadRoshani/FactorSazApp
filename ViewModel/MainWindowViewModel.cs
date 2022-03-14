// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="MainWindowViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Regions;

    using WaybillApp.Core;
    using WaybillApp.Core.Region;
    using WaybillApp.Data;
    using WaybillApp.View;

    /// <summary>
    /// Class MainWindowViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// The page content
        /// </summary>
        private object pageContent;

        private KeyValuePair<string, string> selectedTab;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.regionManager = regionManager;
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) => this.RaisePropertyChanged(nameof(this.Time));
            timer.Start();

            regionManager.Regions.CollectionChanged += this.Regions_CollectionChanged;

            this.PageContent = this; // this.LoginViewModel;

            this.ChangePageCommand = new DelegateCommand<string>(this.ChangePage);
            this.OpenSmsPanelCommand = new DelegateCommand(
                () =>
                    {
                        new SmsPanelWindow { DataContext = this.SmsPanelViewModel }.ShowDialog();
                    });

            LocalData.Create(eventAggregator, unitOfWorkExtended).LoadEvents().LoadData();
            this.TabItems = new Dictionary<string, string>
                                {
                                    { nameof(OutboundInvoiceView), "بارنامه خروجی" },
                                    { nameof(OutboundInvoiceListView), "لیست بارنامه" },
                                    { nameof(InboundInvoiceView), "کالای ورودی" },
                                    { nameof(InboundInvoiceListView), "لیست ورودی" },
                                    { nameof(CodingView), "‌کد‌گذاری" },
                                };
        }

        /// <summary>
        /// Gets the SMS panel view model.
        /// </summary>
        public SmsPanelViewModel SmsPanelViewModel { get; } = new SmsPanelViewModel();

        /// <summary>
        /// Gets or sets the change page command.
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        /// <summary>
        /// Gets or sets the open SMS panel command.
        /// </summary>
        public DelegateCommand OpenSmsPanelCommand { get; set; }

        /// <summary>
        /// Gets the tab items.
        /// </summary>
        public Dictionary<string, string> TabItems { get; }

        /// <summary>
        /// Gets or sets the selected tab.
        /// </summary>
        public KeyValuePair<string, string> SelectedTab
        {
            get => this.selectedTab;
            set => this.SetProperty(ref this.selectedTab, value);
        }

        /// <summary>
        /// Gets or sets the content of the page.
        /// </summary>
        /// <value>The content of the page.</value>
        public object PageContent
        {
            get => this.pageContent;
            set => this.SetProperty(ref this.pageContent, value);
        }

        /// <summary>
        /// Gets a value indicating whether [show navigate bar].
        /// </summary>
        /// <value><c>true</c> if [show navigate bar]; otherwise, <c>false</c>.</value>
        public bool ShowNavigateBar => this.SelectedTab.Key != nameof(LoginView);

        /// <summary>
        /// Gets the time.
        /// </summary>
        public string Time => PersianDateTime.Now.ToString("dddd yyyy/MM/dd\nHH:mm:ss");

        /// <summary>
        /// Changes the page.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ChangePage(string parameter)
        {
            this.UpdatePageContent(parameter);
        }

        /// <summary>
        /// Updates the content of the page.
        /// </summary>
        /// <param name="source">The source.</param>
        private void UpdatePageContent(string source)
        {
            this.regionManager.RequestNavigate(RegionNames.MainRegion, source);

            this.RaisePropertyChanged(nameof(this.ShowNavigateBar));
        }

        private void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var region = (IRegion)e.NewItems[0];
                region.Views.CollectionChanged += this.Views_CollectionChanged;
            }
        }

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var view = (UserControl)e.NewItems[0];
                view.Loaded += (o, args) => this.SelectedTab = this.TabItems.FirstOrDefault(x => x.Key == view.GetType().Name);
            }
        }
    }
}