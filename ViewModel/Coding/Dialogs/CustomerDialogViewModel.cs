// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-20-2020
//
// Last Modified By : Artan
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="CustomerDialogViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.Coding.Dialogs
{
    using System;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using WaybillApp.Core;
    using WaybillApp.Model;

    /// <summary>
    /// Class CustomerDialogViewModel.
    /// Implements the <see cref="Prism.Mvvm.BindableBase" />
    /// </summary>
    public class CustomerDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        /// <summary>
        /// The has error adding customer
        /// </summary>
        private bool hasErrorAddingCustomer;

        /// <summary>
        /// The new customer code
        /// </summary>
        private string newCustomerCode;

        /// <summary>
        /// The new customer name
        /// </summary>
        private string newCustomerName;

        /// <summary>
        /// The new customer national identifier
        /// </summary>
        private string newCustomerNationalId;

        /// <summary>
        /// The new customer phone
        /// </summary>
        private string newCustomerPhone;

        public CustomerDialogViewModel(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.AddNewItemCommand = new DelegateCommand(this.AddNewCustomer);
        }

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// Gets the add new customer command.
        /// </summary>
        public DelegateCommand AddNewItemCommand { get; }

        /// <summary>
        /// The title of the dialog that will show in the Window title bar.
        /// </summary>
        public string Title => "افزودن مشتری";

        /// <summary>
        /// Gets or sets a value indicating whether this instance has error adding customer.
        /// </summary>
        /// <value><c>true</c> if this instance has error adding customer; otherwise, <c>false</c>.</value>
        public bool HasErrorAddingCustomer
        {
            get => this.hasErrorAddingCustomer;
            set => this.SetProperty(ref this.hasErrorAddingCustomer, value);
        }

        /// <summary>
        /// Gets or sets the new customer code.
        /// </summary>
        public string NewCustomerCode
        {
            get => this.newCustomerCode;
            set => this.SetProperty(ref this.newCustomerCode, value);
        }

        /// <summary>
        /// Gets or sets the  new customer name.
        /// </summary>
        /// <value>The new name of the customer.</value>
        public string NewCustomerName
        {
            get => this.newCustomerName;
            set => this.SetProperty(ref this.newCustomerName, value);
        }

        /// <summary>
        /// Gets or sets the  new customer national id.
        /// </summary>
        public string NewCustomerNationalId
        {
            get => this.newCustomerNationalId;
            set => this.SetProperty(ref this.newCustomerNationalId, value);
        }

        /// <summary>
        /// Gets or sets the  new customer phone.
        /// </summary>
        public string NewCustomerPhone
        {
            get => this.newCustomerPhone;
            set => this.SetProperty(ref this.newCustomerPhone, value);
        }

        /// <summary>
        /// Determines if the dialog can be closed.
        /// </summary>
        /// <returns>True: close the dialog; False: the dialog will not close</returns>
        public bool CanCloseDialog() => true;

        /// <summary>
        /// Called when the dialog is closed.
        /// </summary>
        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// Called when the dialog is opened.
        /// </summary>
        /// <param name="parameters">The parameters passed to the dialog</param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        /// <summary>
        /// Adds the new customer.
        /// </summary>
        private async void AddNewCustomer()
        {
            if (string.IsNullOrEmpty(this.NewCustomerCode) || this.unitOfWorkExtended.Customers.GetByCode(this.NewCustomerCode) != null)
            {
                this.HasErrorAddingCustomer = true;
                return;
            }

            var newCustomer = new Customer
                                  {
                                      FullName = this.NewCustomerName,
                                      Code = this.NewCustomerCode,
                                      PhoneNumber = this.NewCustomerPhone,
                                      NationalId = this.NewCustomerNationalId
                                  };
            var item = await this.unitOfWorkExtended.Customers.InsertAsync(newCustomer);
            await this.unitOfWorkExtended.SaveChangesAsync();
            if (item?.Entity != null)
            {
                this.HasErrorAddingCustomer = false;
                this.NewCustomerCode = string.Empty;
                this.NewCustomerNationalId = string.Empty;
                this.NewCustomerName = string.Empty;
                this.NewCustomerPhone = string.Empty;
                this.NewCustomerNationalId = string.Empty;
            }
            else
            {
                this.HasErrorAddingCustomer = true;
            }

            this.eventAggregator.GetEvent<PubSubEvents.AddCustomer>().Publish(newCustomer);
        }
    }
}
