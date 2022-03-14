// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-02-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CalendarComponent.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Class Calendar.
    /// Implements the <see cref="UserControl" />
    /// </summary>
    public partial class CalendarComponent : UserControl
    {
        /// <summary>
        /// The date property
        /// </summary>
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime?), typeof(CalendarComponent), new PropertyMetadata(DateTime.Now));

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarComponent"/> class.
        /// </summary>
        public CalendarComponent() => this.InitializeComponent();

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? Date
        {
            get => (DateTime?)this.GetValue(DateProperty);
            set => this.SetValue(DateProperty, value);
        }
    }
}
