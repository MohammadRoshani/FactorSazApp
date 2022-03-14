// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="WareRepository.cs" company="WaybillApp">
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
    /// Class WareRepository.
    /// Implements the <see cref="IWareRepository" />
    /// </summary>
    /// <seealso cref="IWareRepository" />
    public class WareRepository : Repository<Ware>, IWareRepository
    {
        public WareRepository(DbContext context)
            : base(context)
        {
        }

        public Ware GetByCode(string code)
        {
            return this._dbContext.Set<Ware>().Find(code);
        }
    }
}