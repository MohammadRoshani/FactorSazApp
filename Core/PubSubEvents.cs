// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-02-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="PubSubEvents.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core
{
    using System.Collections.Generic;

    using Prism.Events;

    using WaybillApp.Model;

    /// <summary>
    /// Class OutboundInvoiceSelectedEvent.
    /// Implements the <see cref="PubSubEvent{OutboundInvoice}" />
    /// </summary>
    /// <seealso cref="PubSubEvent{OutboundInvoice}" />
    public static class PubSubEvents
    {
        public class SelectOutboundInvoice : PubSubEvent<OutboundInvoice>
        {
        }

        public class AddOutboundInvoice : PubSubEvent<OutboundInvoice>
        {
        }

        public class UpdateOutboundInvoice : PubSubEvent<OutboundInvoice>
        {
        }

        public class DeleteOutboundInvoice : PubSubEvent<OutboundInvoice>
        {
        }

        public class AddInboundInvoice : PubSubEvent<InboundInvoice>
        {
        }

        public class UpdateInboundInvoice : PubSubEvent<InboundInvoice>
        {
        }

        public class DeleteInboundInvoice : PubSubEvent<InboundInvoice>
        {
        }

        public class LoadCustomers : PubSubEvent<IEnumerable<Customer>>
        {
        }

        public class AddCustomer : PubSubEvent<Customer>
        {
        }

        public class DeleteCustomer : PubSubEvent<Customer>
        {
        }

        public class LoadLocations : PubSubEvent<IEnumerable<Location>>
        {
        }

        public class AddLocation : PubSubEvent<Location>
        {
        }

        public class DeleteLocation : PubSubEvent<Location>
        {
        }

        public class LoadWares : PubSubEvent<IEnumerable<Ware>>
        {
        }

        public class AddWare : PubSubEvent<Ware>
        {
        }

        public class DeleteWare : PubSubEvent<Ware>
        {
        }
    }
}