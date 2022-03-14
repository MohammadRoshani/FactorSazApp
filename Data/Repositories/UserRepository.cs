// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="UserRepository.cs" company="WaybillApp">
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
    /// Class UserRepository.
    /// Implements the <see cref="IUserRepository" />
    /// </summary>
    /// <seealso cref="IUserRepository" />
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}