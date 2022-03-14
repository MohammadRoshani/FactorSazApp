// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-20-2020
//
// Last Modified By : Artan
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LocationDialogViewModel.cs" company="WaybillApp">
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

    public class LocationDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        /// <summary>
        /// The has error adding location
        /// </summary>
        private bool hasErrorAddingLocation;

        /// <summary>
        /// The new location city
        /// </summary>
        private string newLocationCity;

        /// <summary>
        /// The new location code
        /// </summary>
        private string newLocationCode;

        /// <summary>
        /// The new location discharge
        /// </summary>
        private string newLocationDischarge;

        public LocationDialogViewModel(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.AddNewItemCommand = new DelegateCommand(this.AddNewLocation);
        }

        public event Action<IDialogResult> RequestClose;

        public string Title { get; }

        /// <summary>
        /// Gets the add new location command.
        /// </summary>
        public DelegateCommand AddNewItemCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has error adding location.
        /// </summary>
        /// <value><c>true</c> if this instance has error adding location; otherwise, <c>false</c>.</value>
        public bool HasErrorAddingLocation
        {
            get => this.hasErrorAddingLocation;
            set => this.SetProperty(ref this.hasErrorAddingLocation, value);
        }

        /// <summary>
        /// Gets or sets the  new location city.
        /// </summary>
        public string NewLocationCity
        {
            get => this.newLocationCity;
            set => this.SetProperty(ref this.newLocationCity, value);
        }

        /// <summary>
        /// Gets or sets the  new location code.
        /// </summary>
        public string NewLocationCode
        {
            get => this.newLocationCode;
            set => this.SetProperty(ref this.newLocationCode, value);
        }

        /// <summary>
        /// Gets or sets the  new location discharge.
        /// </summary>
        public string NewLocationDischarge
        {
            get => this.newLocationDischarge;
            set => this.SetProperty(ref this.newLocationDischarge, value);
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
        /// Adds the new location.
        /// </summary>
        private async void AddNewLocation()
        {
            if (this.unitOfWorkExtended.Locations.GetByCode(this.NewLocationCode) != null)
            {
                this.HasErrorAddingLocation = true;
                return;
            }

            var newLocation = new Location
                                  {
                                      City = this.NewLocationCity,
                                      Code = this.NewLocationCode,
                                      Discharge = this.NewLocationDischarge
                                  };
            var item = await this.unitOfWorkExtended.Locations.InsertAsync(newLocation);
            await this.unitOfWorkExtended.SaveChangesAsync();
            if (item?.Entity != null)
            {
                this.HasErrorAddingLocation = false;
                this.NewLocationDischarge = string.Empty;
                this.NewLocationCity = string.Empty;
                this.NewLocationCode = string.Empty;
            }
            else
            {
                this.HasErrorAddingLocation = true;
            }

            this.eventAggregator.GetEvent<PubSubEvents.AddLocation>().Publish(newLocation);
        }
    }
}