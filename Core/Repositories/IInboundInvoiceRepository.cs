// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="IInboundInvoiceRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core.Repositories
{
    using System.Threading.Tasks;

    using Arch.EntityFrameworkCore.UnitOfWork;

    using WaybillApp.Model;

    /// <summary>
    /// Interface IInvoicesRepository
    /// </summary>
    public interface IInboundInvoiceRepository : IRepository<InboundInvoice>
    {
        Task<int> MaxBillWayCodeAsync();
    }
}