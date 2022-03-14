// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-28-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-28-2019
// ***********************************************************************
// <copyright file="IUnitOfWorkExtended.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core
{
    using Arch.EntityFrameworkCore.UnitOfWork;

    using WaybillApp.Core.Repositories;

    public interface IUnitOfWorkExtended : IUnitOfWork
    {
        ICustomersRepository Customers { get; }

        IInboundInvoiceRepository InboundInvoices { get; }

        ILocationRepository Locations { get; }

        IUserRepository Users { get; }

        IWareRepository Wares { get; }

        IOutboundInvoiceRepository OutboundInvoices { get; }
    }
}