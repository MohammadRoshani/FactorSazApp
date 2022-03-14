// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 12-28-2019
//
// Last Modified By : Artan
// Last Modified On : 01-19-2020
// ***********************************************************************
// <copyright file="OutboundInvoiceRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Data.Repositories
{
    using System.Threading.Tasks;

    using Arch.EntityFrameworkCore.UnitOfWork;

    using Microsoft.EntityFrameworkCore;

    using WaybillApp.Core.Repositories;
    using WaybillApp.Model;

    public class OutboundInvoiceRepository : Repository<OutboundInvoice>, IOutboundInvoiceRepository
    {
        public OutboundInvoiceRepository(DbContext context)
            : base(context)
        {
        }

        public Task<int> MaxBillWayCodeAsync() => this._dbContext.Set<OutboundInvoice>().MaxAsync(x => x.BillWayCode);
    }
}