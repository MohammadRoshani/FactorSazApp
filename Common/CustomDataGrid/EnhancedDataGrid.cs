// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-04-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="EnhancedDataGrid.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Common.CustomDataGrid
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Class EnhancedDataGrid.
    /// Implements the <see cref="DataGrid" />
    /// </summary>
    /// <seealso cref="DataGrid" />
    public class EnhancedDataGrid : DataGrid
    {
        /// <summary>
        /// Gets the set columns property.
        /// </summary>
        public static DependencyProperty SetColumnsProperty { get; } = DependencyProperty.Register(
            "SetColumns",
            typeof(ObservableCollection<DataGridColumn>),
            typeof(EnhancedDataGrid),
            new FrameworkPropertyMetadata
                {
                    DefaultValue = new ObservableCollection<DataGridColumn>(),
                    PropertyChangedCallback = SetColumnsChanged,
                    AffectsRender = true,
                    AffectsMeasure = true,
                    AffectsParentMeasure = true,
                    IsAnimationProhibited = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                });

        /// <summary>
        /// Gets or sets the set columns.
        /// </summary>
        public ObservableCollection<DataGridColumn> SetColumns
        {
            get => (ObservableCollection<DataGridColumn>)this.GetValue(SetColumnsProperty);
            set => this.SetValue(SetColumnsProperty, value);
        }

        /// <summary>
        /// Sets the columns changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void SetColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = (DataGrid)d;

            dataGrid.Columns.Clear();
            foreach (var column in (ObservableCollection<DataGridColumn>)e.NewValue)
            {
                dataGrid.Columns.Add(column);
            }
        }
    }
}
