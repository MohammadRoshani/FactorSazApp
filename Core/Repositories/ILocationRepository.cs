﻿// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="ILocationRepository.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Core.Repositories
{
    using Arch.EntityFrameworkCore.UnitOfWork;

    using WaybillApp.Model;

    /// <summary>
    /// Interface ILocationRepository
    /// </summary>
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetByCode(string code);
    }
}