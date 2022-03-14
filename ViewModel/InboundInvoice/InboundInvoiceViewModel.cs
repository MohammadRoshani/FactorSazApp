// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="InboundInvoiceViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
// ReSharper disable StyleCop.SA1600

// ReSharper disable MemberCanBePrivate.Global

namespace WaybillApp.ViewModel.InboundInvoice
{
    using System;
    using System.Linq;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Regions;

    using WaybillApp.Core;
    using WaybillApp.Data;
    using WaybillApp.Model;
    using WaybillApp.View;

    /// <summary>
    /// Class InboundInvoiceViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class InboundInvoiceViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        private int billWayCode;

        private int count = 1;

        private string description;

        private string driverCode;

        private string driverName;

        private string fare;

        private DateTime inboundDate;

        private DateTime inboundTime;

        private bool isChecked;

        private bool isEditing;

        private string originCityCode;

        private string originCityName;

        private string ownerName;

        private string ownerPhone;

        private string wareCode;

        private string ownerCode;

        private string wareName;

        private InboundInvoice model;

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundInvoiceViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public InboundInvoiceViewModel(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.SaveCommand = new DelegateCommand(this.Save);
            this.CancelCommand = new DelegateCommand(this.Cancel);
            this.CheckCommand = new DelegateCommand<InboundInvoiceViewModel>(this.Check);
            this.SendSmsCommand = new DelegateCommand(this.SendSms);
            this.OpenSmsPanelCommand = new DelegateCommand(
                () => new SmsPanelWindow { DataContext = new SmsPanelViewModel() }.ShowDialog());

            this.InboundDate = DateTime.Now;
            this.InboundTime = DateTime.Now;
            this.SetBillWayCode();
            eventAggregator.GetEvent<PubSubEvents.DeleteInboundInvoice>().Subscribe(invoice => this.Cancel());
        }

        /// <summary>
        /// Gets or sets the bill way code.
        /// </summary>
        public int BillWayCode
        {
            get => this.billWayCode;
            set => this.SetProperty(ref this.billWayCode, value);
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public DelegateCommand CancelCommand { get; }

        /// <summary>
        /// Gets the check command.
        /// </summary>
        public DelegateCommand<InboundInvoiceViewModel> CheckCommand { get; }

        /// <summary>
        /// Gets the open SMS panel command.
        /// </summary>
        public DelegateCommand OpenSmsPanelCommand { get; }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        public DelegateCommand SaveCommand { get; }

        /// <summary>
        /// Gets the send SMS command.
        /// </summary>
        public DelegateCommand SendSmsCommand { get; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count
        {
            get => this.count;
            set => this.SetProperty(ref this.count, value);
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value);
        }

        /// <summary>
        /// Gets or sets the driver code.
        /// </summary>
        public string DriverCode
        {
            get => this.driverCode;
            set
            {
                this.SetProperty(ref this.driverCode, value);
                if (LocalData.CustomersList?.FirstOrDefault(x => x.Code == this.driverCode) is Customer customer)
                {
                    this.DriverName = customer.FullName;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        /// <value>The name of the driver.</value>
        public string DriverName
        {
            get => this.driverName;
            set => this.SetProperty(ref this.driverName, value);
        }

        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        public string Fare
        {
            get => this.fare;
            set => this.SetProperty(ref this.fare, value);
        }

        /// <summary>
        /// Gets or sets the inbound date.
        /// </summary>
        public DateTime InboundDate
        {
            get => this.inboundDate;
            set => this.SetProperty(ref this.inboundDate, value);
        }

        /// <summary>
        /// Gets or sets the inbound time.
        /// </summary>
        public DateTime InboundTime
        {
            get => this.inboundTime;
            set => this.SetProperty(ref this.inboundTime, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked
        {
            get => this.isChecked;
            set => this.SetProperty(ref this.isChecked, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is saved.
        /// </summary>
        /// <value><c>true</c> if this instance is saved; otherwise, <c>false</c>.</value>
        public bool IsEditing
        {
            get => this.isEditing;
            set => this.SetProperty(ref this.isEditing, value);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public InboundInvoice Model
        {
            get => this.model;
            set
            {
                this.SetProperty(ref this.model, value);
                this.InBoundInvoiceId = this.model.Id;
                this.BillWayCode = value.BillWayCode;
                this.Count = int.TryParse(value.Count, out var c) ? c : 0;
                this.InboundDate = value.Date;
                this.InboundTime = value.Time;
                this.Description = value.Description;
                this.DriverCode = value.DriverCode;
                this.DriverName = value.DriverName;
                this.Fare = value.Fare;
                this.OriginCityCode = value.OriginCityCode;
                this.OwnerPhone = value.OwnerPhone;
                this.OwnerName = value.OwnerName;
                this.WareName = value.WareName;
                this.OriginCityName = value.OriginCityName;
                this.IsChecked = value.IsChecked;
            }
        }

        /// <summary>
        /// Gets or sets the in bound invoice identifier.
        /// </summary>
        public long InBoundInvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the origin city code.
        /// </summary>
        public string OriginCityCode
        {
            get => this.originCityCode;
            set
            {
                this.SetProperty(ref this.originCityCode, value);
                if (LocalData.LocationsList?.FirstOrDefault(x => x.Code == this.originCityCode) is Location location)
                {
                    this.OriginCityName = location.City;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the origin city.
        /// </summary>
        /// <value>The name of the origin city.</value>
        public string OriginCityName
        {
            get => this.originCityName;
            set => this.SetProperty(ref this.originCityName, value);
        }

        /// <summary>
        /// Gets or sets the owner code.
        /// </summary>
        public string OwnerCode
        {
            get => this.ownerCode;
            set
            {
                this.SetProperty(ref this.ownerCode, value);
                if (LocalData.CustomersList?.FirstOrDefault(x => x.Code == this.ownerCode) is Customer customer)
                {
                    this.OwnerName = customer.FullName;
                    this.OwnerPhone = customer.PhoneNumber;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        /// <value>The name of the owner.</value>
        public string OwnerName
        {
            get => this.ownerName;
            set => this.SetProperty(ref this.ownerName, value);
        }

        /// <summary>
        /// Gets or sets the owner phone.
        /// </summary>
        public string OwnerPhone
        {
            get => this.ownerPhone;
            set => this.SetProperty(ref this.ownerPhone, value);
        }

        /// <summary>
        /// Gets or sets the ware code.
        /// </summary>
        public string WareCode
        {
            get => this.wareCode;
            set
            {
                this.SetProperty(ref this.wareCode, value);
                if (LocalData.WaresList?.FirstOrDefault(x => x.Code == this.wareCode) is Ware ware)
                {
                    this.WareName = ware.Name;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the ware.
        /// </summary>
        /// <value>The name of the ware.</value>
        public string WareName
        {
            get => this.wareName;
            set => this.SetProperty(ref this.wareName, value);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["IInvoice"] is InboundInvoice invoice)
            {
                this.Model = invoice;
                this.IsEditing = true;
            }
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns><see langword="true" /> if this instance accepts the navigation request; otherwise, <see langword="false" />.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// Checks the specified invoice view model.
        /// </summary>
        /// <param name="invoiceViewModel">The invoice view model.</param>
        public void Check(InboundInvoiceViewModel invoiceViewModel)
        {
            invoiceViewModel.IsChecked = !invoiceViewModel.IsChecked;

            // MainWindowViewModel.InboundInvoiceViewModels[
            // MainWindowViewModel.InboundInvoiceViewModels.IndexOf(invoiceViewModel)] = invoiceViewModel;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public async void Save()
        {
            this.Model = this.CreateInboundInvoice();
            var isUpdate = this.IsEditing && this.Model.Id != 0;
            if (isUpdate)
            {
                this.unitOfWorkExtended.InboundInvoices.Update(this.Model);
            }
            else
            {
                await this.unitOfWorkExtended.InboundInvoices.InsertAsync(this.CreateInboundInvoice());
            }

            await this.unitOfWorkExtended.SaveChangesAsync();

            this.IsEditing = true;
            if (isUpdate)
            {
                this.eventAggregator.GetEvent<PubSubEvents.UpdateInboundInvoice>().Publish(this.Model);
            }
            else
            {
                this.eventAggregator.GetEvent<PubSubEvents.AddInboundInvoice>().Publish(this.Model);
            }
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        private void Cancel() => this.CreateBlankInvoice();

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        private void SendSms()
        {
            new SmsPanelWindow { DataContext = new SmsPanelViewModel() }.ShowDialog();
        }

        private void CreateBlankInvoice()
        {
            this.Model = new InboundInvoice
                             {
                                 Date = DateTime.Now,
                                 Time = DateTime.Now,
                             };
            this.IsEditing = false;
            this.DriverCode = string.Empty;
            this.OwnerCode = string.Empty;
            this.WareCode = string.Empty;
            this.SetBillWayCode();
        }

        private async void SetBillWayCode() =>
            this.BillWayCode = await this.unitOfWorkExtended.InboundInvoices.MaxBillWayCodeAsync() + 1;

        private InboundInvoice CreateInboundInvoice() =>
            new InboundInvoice
                {
                    Id = this.InBoundInvoiceId,
                    BillWayCode = this.BillWayCode,
                    Count = $"{this.Count}",
                    Date = this.InboundDate,
                    Time = this.InboundTime,
                    Description = this.Description,
                    DriverCode = this.DriverCode,
                    DriverName = this.DriverName,
                    Fare = this.Fare,
                    OriginCityCode = this.OriginCityCode,
                    OwnerPhone = this.OwnerPhone,
                    OwnerName = this.OwnerName,
                    WareName = this.WareName,
                    OriginCityName = this.OriginCityName,
                    IsChecked = this.IsChecked
                };
    }
}