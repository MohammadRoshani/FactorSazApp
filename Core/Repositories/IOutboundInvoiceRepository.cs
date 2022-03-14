// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-28-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-28-2019
// ***********************************************************************
// <copyright file="IOutboundInvoiceRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core.Repositories
{
    using System.Threading.Tasks;

    using Arch.EntityFrameworkCore.UnitOfWork;

    using WaybillApp.Model;

    public interface IOutboundInvoiceRepository : IRepository<OutboundInvoice>
    {
        Task<int> MaxBillWayCodeAsync();
    }
}
