// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-07-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="App.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Markup;

    using Microsoft.EntityFrameworkCore;

    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Regions;
    using Prism.Services.Dialogs;
    using Prism.Unity;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using WaybillApp.Component;
    using WaybillApp.Core;
    using WaybillApp.Core.Region;
    using WaybillApp.Data;
    using WaybillApp.Services;
    using WaybillApp.View;
    using WaybillApp.View.Dialogs;
    using WaybillApp.ViewModel;
    using WaybillApp.ViewModel.Coding;
    using WaybillApp.ViewModel.Coding.Dialogs;
    using WaybillApp.ViewModel.Component.Dialogs;
    using WaybillApp.ViewModel.InboundInvoice;
    using WaybillApp.ViewModel.OutboundInvoice;

    /// <summary>
    /// Class App.
    /// Implements the <see cref="PrismApplication" />
    /// </summary>
    /// <seealso cref="PrismApplication" />
    public partial class App : PrismApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            using (var db = new DatabaseContext())
            {
                db.Database.Migrate();
            }

            // SmsPanelIp = File.ReadAllText("SmsPanelIP.ini");
            var cultureInfo = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        /// <summary>
        /// Gets or sets the SMS panel ip.
        /// </summary>
        public static string SmsPanelIp { get; set; }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override Window CreateShell() => this.Container.Resolve<MainWindow>();

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<OutboundInvoiceView>();
            containerRegistry.RegisterForNavigation<OutboundInvoiceListView>();
            containerRegistry.RegisterForNavigation<InboundInvoiceView>();
            containerRegistry.RegisterForNavigation<InboundInvoiceListView>();
            containerRegistry.RegisterForNavigation<CodingView>();

            var unityContainer = this.Container.Resolve<IUnityContainer>();
            unityContainer.RegisterType<DbContext, DatabaseContext>(this.Container.Resolve<ContainerControlledLifetimeManager>());
            unityContainer.RegisterType<IUnitOfWorkExtended, UnitOfWorkExtended>(this.Container.Resolve<ContainerControlledLifetimeManager>());
            unityContainer.RegisterType<IDialogService, PopupDialogService>(this.Container.Resolve<ContainerControlledLifetimeManager>());

            containerRegistry.RegisterDialog<LocationDialog>();
            containerRegistry.RegisterDialog<CustomerDialog>();
            containerRegistry.RegisterDialog<WareDialog>();
            containerRegistry.RegisterDialog<TimeDialog>();
            containerRegistry.RegisterDialog<CalendarDialog>();
        }

        /// <summary>
        /// Contains actions that should occur last.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Container.Resolve<IRegionManager>().RequestNavigate(RegionNames.MainRegion, nameof(OutboundInvoiceView));
        }

        /// <summary>
        /// Configures the <see cref="T:Prism.Mvvm.ViewModelLocator" /> used by Prism.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<OutboundInvoiceView, OutboundInvoiceViewModel>();
            ViewModelLocationProvider.Register<OutboundInvoicePrintView, OutboundInvoicePrintViewModel>();
            ViewModelLocationProvider.Register<OutboundInvoiceListView, OutboundInvoiceListViewModel>();
            ViewModelLocationProvider.Register<InboundInvoiceView, InboundInvoiceViewModel>();
            ViewModelLocationProvider.Register<InboundInvoiceListView, InboundInvoiceListViewModel>();
            ViewModelLocationProvider.Register<CodingView, CodingViewModel>();
            ViewModelLocationProvider.Register<LocationDialog, LocationDialogViewModel>();
            ViewModelLocationProvider.Register<CustomerDialog, CustomerDialogViewModel>();
            ViewModelLocationProvider.Register<WareDialog, WareDialogViewModel>();
            ViewModelLocationProvider.Register<TimeComponent, DateTimeDialogViewModel>();
            ViewModelLocationProvider.Register<TimeDialog, DateTimeDialogViewModel>();
            ViewModelLocationProvider.Register<CalendarDialog, DateTimeDialogViewModel>();
        }
    }
}