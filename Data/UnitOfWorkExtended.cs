// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-28-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-28-2019
// ***********************************************************************
// <copyright file="UnitOfWorkExtended.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Data
{
    using Arch.EntityFrameworkCore.UnitOfWork;

    using Microsoft.EntityFrameworkCore;

    using WaybillApp.Core;
    using WaybillApp.Core.Repositories;
    using WaybillApp.Data.Repositories;

    public class UnitOfWorkExtended : UnitOfWork<DbContext>, IUnitOfWorkExtended
    {
        public UnitOfWorkExtended(DbContext context)
            : base(context)
        {
            this.Customers = new CustomersRepository(context);
            this.InboundInvoices = new InboundInvoiceRepository(context);
            this.Locations = new LocationRepository(context);
            this.Users = new UserRepository(context);
            this.Wares = new WareRepository(context);
            this.OutboundInvoices = new OutboundInvoiceRepository(context);
        }

        public ICustomersRepository Customers { get; }

        public IInboundInvoiceRepository InboundInvoices { get; }

        public ILocationRepository Locations { get; }

        public IUserRepository Users { get; }

        public IWareRepository Wares { get; }

        public IOutboundInvoiceRepository OutboundInvoices { get; }
    }
}