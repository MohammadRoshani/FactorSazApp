// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-20-2020
//
// Last Modified By : Artan
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="WareDialogViewModel.cs" company="WaybillApp">
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

    public class WareDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        /// <summary>
        /// The has error adding ware
        /// </summary>
        private bool hasErrorAddingWare;

        /// <summary>
        /// The new ware code
        /// </summary>
        private string newWareCode;

        /// <summary>
        /// The new ware name
        /// </summary>
        private string newWareName;

        /// <summary>
        /// Initializes a new instance of the <see cref="WareCodingViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        public WareDialogViewModel(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;

            this.AddNewItemCommand = new DelegateCommand(this.AddNewWare);
        }

        public event Action<IDialogResult> RequestClose;

        public string Title { get; }

        /// <summary>
        /// Gets the add new ware command.
        /// </summary>
        public DelegateCommand AddNewItemCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has error adding ware.
        /// </summary>
        /// <value><c>true</c> if this instance has error adding ware; otherwise, <c>false</c>.</value>
        public bool HasErrorAddingWare
        {
            get => this.hasErrorAddingWare;
            set => this.SetProperty(ref this.hasErrorAddingWare, value);
        }

        /// <summary>
        /// Gets or sets the  new ware code.
        /// </summary>
        public string NewWareCode
        {
            get => this.newWareCode;
            set => this.SetProperty(ref this.newWareCode, value);
        }

        /// <summary>
        /// Gets or sets the  new ware name.
        /// </summary>
        /// <value>The new name of the ware.</value>
        public string NewWareName
        {
            get => this.newWareName;
            set => this.SetProperty(ref this.newWareName, value);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        /// <summary>
        /// Adds the new ware.
        /// </summary>
        private async void AddNewWare()
        {
            if (this.unitOfWorkExtended.Wares.GetByCode(this.NewWareCode) != null)
            {
                this.HasErrorAddingWare = true;
                return;
            }

            var newWare = new Ware { Name = this.NewWareName, Code = this.NewWareCode };
            var item = await this.unitOfWorkExtended.Wares.InsertAsync(newWare);
            await this.unitOfWorkExtended.SaveChangesAsync();
            if (item?.Entity != null)
            {
                this.HasErrorAddingWare = false;
                this.NewWareCode = string.Empty;
                this.NewWareName = string.Empty;
            }
            else
            {
                this.HasErrorAddingWare = true;
            }

            this.eventAggregator.GetEvent<PubSubEvents.AddWare>().Publish(newWare);
        }
    }
}