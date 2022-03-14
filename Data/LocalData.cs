// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-08-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-08-2020
// ***********************************************************************
// <copyright file="LocalData.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Data
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Prism.Events;

    using WaybillApp.Core;
    using WaybillApp.Model;

    public class LocalData
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalData" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work.</param>
        private LocalData(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
        }

        /// <summary>
        /// Gets the locations list.
        /// </summary>
        public static ObservableCollection<Location> LocationsList { get; } = new ObservableCollection<Location>();

        /// <summary>
        /// Gets the customers list.
        /// </summary>
        public static ObservableCollection<Customer> CustomersList { get; } = new ObservableCollection<Customer>();

        /// <summary>
        /// Gets the wares list.
        /// </summary>
        public static ObservableCollection<Ware> WaresList { get; } = new ObservableCollection<Ware>();

        public static LocalData Create(IEventAggregator eventAggregator, IUnitOfWorkExtended unitOfWorkExtended) => new LocalData(eventAggregator, unitOfWorkExtended);

        /// <summary>
        /// Loads the events.
        /// </summary>
        /// <returns>Local Data.</returns>
        public LocalData LoadEvents()
        {
            this.eventAggregator.GetEvent<PubSubEvents.AddCustomer>()
                .Subscribe(newCustomer => CustomersList.Add(newCustomer));
            this.eventAggregator.GetEvent<PubSubEvents.LoadCustomers>().Subscribe(
                newCustomers =>
                    {
                        CustomersList.Clear();
                        foreach (var newCustomer in newCustomers)
                        {
                            CustomersList.Add(newCustomer);
                        }
                    });
            this.eventAggregator.GetEvent<PubSubEvents.DeleteCustomer>().Subscribe(
                deletedCustomer =>
                    {
                        if (CustomersList.FirstOrDefault(x => x.Code == deletedCustomer.Code) is Customer item)
                        {
                            CustomersList.Remove(item);
                        }
                    });

            this.eventAggregator.GetEvent<PubSubEvents.AddLocation>()
                .Subscribe(newLocation => LocationsList.Add(newLocation));
            this.eventAggregator.GetEvent<PubSubEvents.LoadLocations>().Subscribe(
                newLocations =>
                    {
                        LocationsList.Clear();
                        foreach (var newLocation in newLocations)
                        {
                            LocationsList.Add(newLocation);
                        }
                    });
            this.eventAggregator.GetEvent<PubSubEvents.DeleteLocation>().Subscribe(
                deletedLocation =>
                    {
                        if (LocationsList.FirstOrDefault(x => x.Code == deletedLocation.Code) is Location item)
                        {
                            LocationsList.Remove(item);
                        }
                    });

            this.eventAggregator.GetEvent<PubSubEvents.AddWare>().Subscribe(newWare => WaresList.Add(newWare));
            this.eventAggregator.GetEvent<PubSubEvents.LoadWares>().Subscribe(
                newWares =>
                    {
                        WaresList.Clear();
                        foreach (var newWare in newWares)
                        {
                            WaresList.Add(newWare);
                        }
                    });
            this.eventAggregator.GetEvent<PubSubEvents.DeleteWare>().Subscribe(
                deletedWare =>
                    {
                        if (WaresList.FirstOrDefault(x => x.Code == deletedWare.Code) is Ware item)
                        {
                            WaresList.Remove(item);
                        }
                    });

            return this;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns>Local Data.</returns>
        public LocalData LoadData()
        {
#pragma warning disable 618
            this.eventAggregator.GetEvent<PubSubEvents.LoadCustomers>().Publish(this.unitOfWorkExtended.Customers.GetAll());
            this.eventAggregator.GetEvent<PubSubEvents.LoadLocations>().Publish(this.unitOfWorkExtended.Locations.GetAll());
            this.eventAggregator.GetEvent<PubSubEvents.LoadWares>().Publish(this.unitOfWorkExtended.Wares.GetAll());
#pragma warning restore 618

            return this;
        }
    }
}
