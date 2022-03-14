// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="CodingViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.Coding
{
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using WaybillApp.Core;
    using WaybillApp.Model;

    /// <summary>
    /// Class CodingViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class CodingViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IUnitOfWorkExtended unitOfWorkExtended;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="unitOfWorkExtended">The unit of work extended.</param>
        /// <param name="dialogService">The dialog service.</param>
        public CodingViewModel(
            IEventAggregator eventAggregator,
            IUnitOfWorkExtended unitOfWorkExtended,
            IDialogService dialogService)
        {
            this.eventAggregator = eventAggregator;
            this.unitOfWorkExtended = unitOfWorkExtended;
            this.OpenDialogCommand =
                new DelegateCommand<string>(dialogName => dialogService.ShowDialog(dialogName, null, null));
            this.DeleteItemCommand = new DelegateCommand<object>(this.DeleteItem);
        }

        /// <summary>
        /// Gets the delete location command.
        /// </summary>
        public ICommand DeleteItemCommand { get; }

        /// <summary>
        /// Gets the delete customer command.
        /// </summary>
        public ICommand OpenDialogCommand { get; }

        private void DeleteItem(object item)
        {
            switch (item)
            {
                case Location location:
                    this.unitOfWorkExtended.Locations.Delete(location);
                    this.eventAggregator.GetEvent<PubSubEvents.DeleteLocation>().Publish(location);
                    break;
                case Customer customer:
                    this.unitOfWorkExtended.Customers.Delete(customer);
                    this.eventAggregator.GetEvent<PubSubEvents.DeleteCustomer>().Publish(customer);
                    break;
                case Ware ware:
                    this.unitOfWorkExtended.Wares.Delete(ware);
                    this.eventAggregator.GetEvent<PubSubEvents.DeleteWare>().Publish(ware);
                    break;
                default:
                    return;
            }

            this.unitOfWorkExtended.SaveChangesAsync();
        }
    }
}