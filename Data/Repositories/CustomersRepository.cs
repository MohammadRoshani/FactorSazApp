// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="CustomersRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Data.Repositories
{
    using Arch.EntityFrameworkCore.UnitOfWork;

    using Microsoft.EntityFrameworkCore;

    using WaybillApp.Core.Repositories;
    using WaybillApp.Model;

    /// <summary>
    /// The customers repository.
    /// Implements the <see cref="ICustomersRepository" />
    /// </summary>
    /// <seealso cref="ICustomersRepository" />
    public class CustomersRepository : Repository<Customer>, ICustomersRepository
    {
        public CustomersRepository(DbContext context)
            : base(context)
        {
        }

        public Customer GetByCode(string code)
        {
            return this._dbContext.Set<Customer>().Find(code);
        }
    }
}