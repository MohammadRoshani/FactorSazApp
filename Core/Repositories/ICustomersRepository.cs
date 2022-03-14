// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="ICustomersRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core.Repositories
{
    using Arch.EntityFrameworkCore.UnitOfWork;

    using WaybillApp.Model;

    /// <summary>
    /// Interface ICustomersRepository
    /// </summary>
    public interface ICustomersRepository : IRepository<Customer>
    {
        Customer GetByCode(string code);
    }
}