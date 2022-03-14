// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-24-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-28-2019
// ***********************************************************************
// <copyright file="BillWayToBarcodeConverter.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Common
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using Neodynamic.WPF;

    internal class BillWayToBarcodeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bc = new BarcodeBuilder
                         {
                             Symbology = Symbology.Code128,
                             Code = value as string,
                             BarcodeUnit = BarcodeUnit.Inch,
                             FitBarcodeToSize = new Size(1.5d, 0.75d)
                         };
            bc.ImageSettings.Dpi = 300;
            bc.ImageSettings.ImageFormat = ImageFormat.Png;
            return bc.GetBarcodeImageSource();
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        /// <exception cref="NotImplementedException">Not ImplementedException</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}