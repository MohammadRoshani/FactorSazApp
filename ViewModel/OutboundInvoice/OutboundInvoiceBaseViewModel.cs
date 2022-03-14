// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-09-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="OutboundInvoiceBaseViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.OutboundInvoice
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Newtonsoft.Json;

    using Prism.Mvvm;

    using WaybillApp.Model;

    public class OutboundInvoiceBaseViewModel : BindableBase
    {
        private int afterFare;

        private int beforeFare;

        private int billWayCode;

        private string destinationCity;

        private string destinationDischarge;

        private DateTime outboundDate;

        private DateTime outboundTime;

        private string receiverFullName;

        private string receiverPhoneNumber;

        private string senderFullName;

        private string senderNationalId;

        private string senderPhoneNumber;

        private int urbanFare;

        private int wage;

        private ObservableCollection<InboundInvoice> wares;

        private OutboundInvoice model;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoiceBaseViewModel"/> class.
        /// </summary>
        public OutboundInvoiceBaseViewModel()
        {
            this.Wares = new ObservableCollection<InboundInvoice>();
            this.OutboundDate = DateTime.Now;
            this.OutboundTime = DateTime.Now;
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
        /// Gets or sets the destination city.
        /// </summary>
        public string DestinationCity
        {
            get => this.destinationCity;
            set => this.SetProperty(ref this.destinationCity, value);
        }

        /// <summary>
        /// Gets or sets the destination discharge.
        /// </summary>
        public string DestinationDischarge
        {
            get => this.destinationDischarge;
            set => this.SetProperty(ref this.destinationDischarge, value);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public OutboundInvoice Model
        {
            get => this.model;
            set
            {
                this.SetProperty(ref this.model, value);
                this.OutBoundInvoiceId = this.model.Id;
                this.SenderFullName = value.SenderFullName;
                this.SenderPhoneNumber = value.SenderPhoneNumber;
                this.SenderNationalId = value.SenderNationalId;
                this.ReceiverFullName = value.ReceiverFullName;
                this.ReceiverPhoneNumber = value.ReceiverPhoneNumber;
                this.UrbanFare = value.UrbanFare;
                this.BeforeFare = value.BeforeFare;
                this.AfterFare = value.AfterFare;
                this.Wage = value.Wage;
                this.BillWayCode = value.BillWayCode;
                this.OutboundDate = value.Date;
                this.OutboundTime = value.Time;
                this.DestinationCity = value.DestinationCity;
                this.DestinationDischarge = value.DestinationDischarge;

                this.Wares = new ObservableCollection<InboundInvoice>(
                    JsonConvert.DeserializeObject<IEnumerable<InboundInvoice>>(string.IsNullOrEmpty(value.Wares) || !value.Wares.StartsWith("[") ? "[]" : value.Wares));
                this.ModelUpdated();
            }
        }

        public long OutBoundInvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the outbound date.
        /// </summary>
        public DateTime OutboundDate
        {
            get => this.outboundDate;
            set => this.SetProperty(ref this.outboundDate, value);
        }

        /// <summary>
        /// Gets or sets the outbound time.
        /// </summary>
        public DateTime OutboundTime
        {
            get => this.outboundTime;
            set => this.SetProperty(ref this.outboundTime, value);
        }

        /// <summary>
        /// Gets or sets the full name of the receiver.
        /// </summary>
        /// <value>The full name of the receiver.</value>
        public string ReceiverFullName
        {
            get => this.receiverFullName;
            set => this.SetProperty(ref this.receiverFullName, value);
        }

        /// <summary>
        /// Gets or sets the receiver phone number.
        /// </summary>
        public string ReceiverPhoneNumber
        {
            get => this.receiverPhoneNumber;
            set => this.SetProperty(ref this.receiverPhoneNumber, value);
        }

        /// <summary>
        /// Gets or sets the full name of the sender.
        /// </summary>
        /// <value>The full name of the sender.</value>
        public string SenderFullName
        {
            get => this.senderFullName;
            set => this.SetProperty(ref this.senderFullName, value);
        }

        /// <summary>
        /// Gets or sets the sender national identifier.
        /// </summary>
        public string SenderNationalId
        {
            get => this.senderNationalId;
            set => this.SetProperty(ref this.senderNationalId, value);
        }

        /// <summary>
        /// Gets or sets the sender phone number.
        /// </summary>
        public string SenderPhoneNumber
        {
            get => this.senderPhoneNumber;
            set => this.SetProperty(ref this.senderPhoneNumber, value);
        }

        /// <summary>
        /// Gets the sum.
        /// </summary>
        public int Sum => this.UrbanFare + this.BeforeFare + this.AfterFare + this.Wage;

        /// <summary>
        /// Gets or sets the urban fare.
        /// </summary>
        public int UrbanFare
        {
            get => this.urbanFare;
            set
            {
                this.SetProperty(ref this.urbanFare, value);
                this.RaisePropertyChanged(nameof(this.Sum));
            }
        }

        /// <summary>
        /// Gets or sets the wage.
        /// </summary>
        public int Wage
        {
            get => this.wage;
            set
            {
                this.SetProperty(ref this.wage, value);
                this.RaisePropertyChanged(nameof(this.Sum));
            }
        }

        /// <summary>
        /// Gets or sets the after fare.
        /// </summary>
        public int AfterFare
        {
            get => this.afterFare;
            set
            {
                this.SetProperty(ref this.afterFare, value);
                this.RaisePropertyChanged(nameof(this.Sum));
            }
        }

        /// <summary>
        /// Gets or sets the before fare.
        /// </summary>
        public int BeforeFare
        {
            get => this.beforeFare;
            set
            {
                this.SetProperty(ref this.beforeFare, value);
                this.RaisePropertyChanged(nameof(this.Sum));
            }
        }

        /// <summary>
        /// Gets or sets the wares.
        /// </summary>
        public ObservableCollection<InboundInvoice> Wares
        {
            get => this.wares;
            set => this.SetProperty(ref this.wares, value);
        }

        protected virtual void ModelUpdated()
        {
        }
    }
}
