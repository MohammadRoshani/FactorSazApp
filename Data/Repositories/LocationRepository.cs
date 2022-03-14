// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="LocationRepository.cs" company="WaybillApp">
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
    /// Class LocationRepository.
    /// Implements the <see cref="ILocationRepository" />
    /// </summary>
    /// <seealso cref="ILocationRepository" />
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext context)
            : base(context)
        {
        }

        public Location GetByCode(string code)
        {
            return this._dbContext.Set<Location>().Find(code);
        }
    }
}