// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-29-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConstantManager.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Class ConstantManager.
    /// </summary>
    public class ConstantManager
    {
        /// <summary>
        /// Gets the costs.
        /// </summary>
        public static IEnumerable<int> Costs { get; }
            = new List<int>
                  {
                      500,
                      1000,
                      1500,
                      2000,
                      2500,
                      3000,
                      3500,
                      4000,
                      4500,
                      5000,
                      5500,
                      6000,
                      6500,
                      7000,
                      7500,
                      8000,
                      8500,
                      9000,
                      9500,
                      10000,
                      11000,
                      12000,
                      13000,
                      14000,
                      15000,
                      16000,
                      17000,
                      18000,
                      19000,
                      20000
                  };

        /// <summary>
        /// Gets the ware count.
        /// </summary>
        public static IEnumerable<int> WareCount { get; }
            = new List<int>
                  {
                      1,
                      2,
                      3,
                      4,
                      5,
                      6,
                      7,
                      8,
                      9,
                      10,
                      11,
                      12,
                      13,
                      14,
                      15,
                      16,
                      17,
                      18,
                      19,
                      20,
                  };
    }
}