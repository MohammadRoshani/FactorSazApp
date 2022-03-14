// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="OutboundInvoiceViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
// ReSharper disable StyleCop.SA1600

// ReSharper disable MemberCanBePrivate.Global

namespace WaybillApp.ViewModel.OutboundInvoice
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;

    using Newtonsoft.Json;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Regions;

    using WaybillApp.Common;
    using WaybillApp.Core;
    using WaybillApp.Data;
    using WaybillApp.Model;
    using WaybillApp.View;

    /// <summary>
    /// Class OutboundInvoiceViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class OutboundInvoiceViewModel : OutboundInvoiceBaseViewModel, INavigationAware
    {
        private readonly IContainerExtension containerExtension;

        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        private bool isEditing;

        private int newItemCount = 1;

        private string newItemName;

        private string locationCode;

        private string receiverCode;

        private string senderCode;

        private string wareCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoiceViewModel" /> class.
        /// </summary>
        /// <param name="containerExtension">The container extension.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public OutboundInvoiceViewModel(IContainerExtension containerExtension, IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.containerExtension = containerExtension;
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.Wares = new ObservableCollection<InboundInvoice>();
            this.AddWareCommand = new DelegateCommand(this.AddWare);
            this.DeleteWareCommand = new DelegateCommand<InboundInvoice>(this.DeleteWare);
            this.PrintCommand = new DelegateCommand(this.Print);
            this.SaveCommand = new DelegateCommand(this.Save);
            this.EditCommand = new DelegateCommand<OutboundInvoice>(this.Edit);
            this.CancelCommand = new DelegateCommand(this.Cancel);
            this.AddNewInvoiceCommand = new DelegateCommand(this.AddNewInvoice);
            this.SendSmsCommand = new DelegateCommand<OutboundInvoice>(this.SendSms);
            this.OpenSmsPanelCommand = new DelegateCommand(
                () => new SmsPanelWindow { DataContext = new SmsPanelViewModel { OutboundInvoiceViewModel = this } }
                    .ShowDialog());
            this.OutboundDate = DateTime.Now;
            this.OutboundTime = DateTime.Now;
            this.SetBillWayCode();
            eventAggregator.GetEvent<PubSubEvents.DeleteOutboundInvoice>().Subscribe(invoice => this.Cancel());
        }

        /// <summary>
        /// Gets the add new invoice command.
        /// </summary>
        public DelegateCommand AddNewInvoiceCommand { get; }

        /// <summary>
        /// Gets the add ware command.
        /// </summary>
        public DelegateCommand AddWareCommand { get; }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public DelegateCommand CancelCommand { get; }

        /// <summary>
        /// Gets the delete ware command.
        /// </summary>
        public DelegateCommand<InboundInvoice> DeleteWareCommand { get; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public DelegateCommand<OutboundInvoice> EditCommand { get; }

        /// <summary>
        /// Gets the open SMS panel command.
        /// </summary>
        public DelegateCommand OpenSmsPanelCommand { get; }

        /// <summary>
        /// Gets the send SMS command.
        /// </summary>
        public DelegateCommand<OutboundInvoice> SendSmsCommand { get; }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        public DelegateCommand SaveCommand { get; }

        /// <summary>
        /// Gets the print command.
        /// </summary>
        public DelegateCommand PrintCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing.
        /// </summary>
        /// <value><c>true</c> if this instance is editing; otherwise, <c>false</c>.</value>
        public bool IsEditing
        {
            get => this.isEditing;
            set => this.SetProperty(ref this.isEditing, value);
        }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        public string LocationCode
        {
            get => this.locationCode;
            set
            {
                this.SetProperty(ref this.locationCode, value);
                if (LocalData.LocationsList.FirstOrDefault(x => x.Code == this.locationCode) is Location location)
                {
                    this.DestinationCity = location.City;
                    this.DestinationDischarge = location.Discharge;
                }
            }
        }

        /// <summary>
        ///  Gets or sets the NewItemCount.
        /// </summary>
        public int NewItemCount
        {
            get => this.newItemCount;
            set => this.SetProperty(ref this.newItemCount, value);
        }

        /// <summary>
        ///  Gets or sets the NewItemName.
        /// </summary>
        public string NewItemName
        {
            get => this.newItemName;
            set => this.SetProperty(ref this.newItemName, value);
        }

        /// <summary>
        /// Gets or sets the receiver code.
        /// </summary>
        public string ReceiverCode
        {
            get => this.receiverCode;
            set
            {
                this.SetProperty(ref this.receiverCode, value);
                if (LocalData.CustomersList.FirstOrDefault(x => x.Code == this.receiverCode) is Customer customer)
                {
                    this.ReceiverFullName = customer.FullName;
                    this.ReceiverPhoneNumber = customer.PhoneNumber;
                }
            }
        }

        /// <summary>
        /// Gets or sets the sender code.
        /// </summary>
        public string SenderCode
        {
            get => this.senderCode;
            set
            {
                this.SetProperty(ref this.senderCode, value);
                if (LocalData.CustomersList.FirstOrDefault(x => x.Code == this.senderCode) is Customer customer)
                {
                    this.SenderFullName = customer.FullName;
                    this.SenderPhoneNumber = customer.PhoneNumber;
                    this.SenderNationalId = customer.NationalId;
                }
            }
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
                if (LocalData.WaresList.FirstOrDefault(x => x.Code == this.wareCode) is Ware ware)
                {
                    this.NewItemName = ware.Name;
                }
            }
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["OInvoice"] is OutboundInvoice invoice)
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
        /// Adds the new invoice.
        /// </summary>
        public void AddNewInvoice() => this.CreateBlankInvoice();

        /// <summary>
        /// Edits the specified invoice view model.
        /// </summary>
        /// <param name="invoiceViewModel">The invoice view model.</param>
        public void Edit(OutboundInvoice invoiceViewModel)
        {
        }

        /// <summary>
        /// Prints the specified invoice view model.
        /// </summary>
        public void Print()
        {
            if (this.containerExtension.Resolve<OutboundInvoicePrintView>() is OutboundInvoicePrintView invoicePrintView)
            {
                this.eventAggregator.GetEvent<PubSubEvents.SelectOutboundInvoice>().Publish(this.CreateOutboundInvoice());
                PrintHelper.ShowPrintPreview(PrintHelper.GetFixedDocument3(invoicePrintView, new PrintDialog()));
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public async void Save()
        {
            this.Model = this.CreateOutboundInvoice();
            var isUpdate = this.IsEditing && this.Model.Id != 0;
            if (isUpdate)
            {
                this.unitOfWorkExtended.OutboundInvoices.Update(this.Model);
            }
            else
            {
                await this.unitOfWorkExtended.OutboundInvoices.InsertAsync(this.CreateOutboundInvoice());
            }

            await this.unitOfWorkExtended.SaveChangesAsync();

            this.IsEditing = true;
            if (isUpdate)
            {
                this.eventAggregator.GetEvent<PubSubEvents.UpdateOutboundInvoice>().Publish(this.Model);
            }
            else
            {
                this.eventAggregator.GetEvent<PubSubEvents.AddOutboundInvoice>().Publish(this.Model);
            }
        }

        /// <summary>
        /// Adds the ware.
        /// </summary>
        private void AddWare()
        {
            if (!string.IsNullOrEmpty(this.NewItemName))
            {
                this.Wares?.Add(new InboundInvoice { Name = this.NewItemName, Count = this.NewItemCount.ToString() });
            }
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        private void Cancel() => this.CreateBlankInvoice();

        /// <summary>
        /// Deletes the ware.
        /// </summary>
        /// <param name="inboundInvoice">The inbound invoice.</param>
        private void DeleteWare(InboundInvoice inboundInvoice)
        {
            this.Wares?.Remove(inboundInvoice);
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="outboundInvoice">The outbound invoice.</param>
        private void SendSms(OutboundInvoice outboundInvoice)
        {
            new SmsPanelWindow { DataContext = new SmsPanelViewModel { OutboundInvoiceViewModel = this } }.ShowDialog();
        }

        private void CreateBlankInvoice()
        {
            this.Model = new OutboundInvoice
                             {
                                 Date = DateTime.Now,
                                 Time = DateTime.Now,
                             };

            this.IsEditing = false;
            this.LocationCode = string.Empty;
            this.ReceiverCode = string.Empty;
            this.SenderCode = string.Empty;
            this.WareCode = string.Empty;
            this.NewItemName = string.Empty;
            this.SetBillWayCode();
        }

        private async void SetBillWayCode() =>
            this.BillWayCode = await this.unitOfWorkExtended.OutboundInvoices.MaxBillWayCodeAsync() + 1;

        private OutboundInvoice CreateOutboundInvoice() =>
            new OutboundInvoice
                {
                    Id = this.OutBoundInvoiceId,
                    SenderFullName = this.SenderFullName,
                    SenderPhoneNumber = this.SenderPhoneNumber,
                    SenderNationalId = this.SenderNationalId,
                    ReceiverFullName = this.ReceiverFullName,
                    ReceiverPhoneNumber = this.ReceiverPhoneNumber,
                    Wares = JsonConvert.SerializeObject(this.Wares),
                    UrbanFare = this.UrbanFare,
                    BeforeFare = this.BeforeFare,
                    AfterFare = this.AfterFare,
                    Wage = this.Wage,
                    BillWayCode = this.BillWayCode,
                    Date = this.OutboundDate,
                    Time = this.OutboundTime,
                    DestinationCity = this.DestinationCity,
                    DestinationDischarge = this.DestinationDischarge
                };
    }
}