// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="InboundInvoiceRepository.cs" company="WaybillApp">
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

    /// <summary>
    /// Class InvoicesRepository.
    /// Implements the <see cref="IInboundInvoiceRepository" />
    /// </summary>
    /// <seealso cref="IInboundInvoiceRepository" />
    public class InboundInvoiceRepository : Repository<InboundInvoice>, IInboundInvoiceRepository
    {
        public InboundInvoiceRepository(DbContext context)
            : base(context)
        {
        }

        public Task<int> MaxBillWayCodeAsync() => this._dbContext.Set<InboundInvoice>().MaxAsync(x => x.BillWayCode);
    }
}