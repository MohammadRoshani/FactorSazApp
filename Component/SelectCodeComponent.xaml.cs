// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-02-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="SelectCodeComponent.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using MaterialDesignThemes.Wpf;

    using WaybillApp.Common.CustomDataGrid;
    using WaybillApp.Model;

    /// <summary>
    /// Class SelectCodeComponent.
    /// Implements the <see cref="object" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    public partial class SelectCodeComponent : InputWithItemsComponent<object>
    {
        /// <summary>
        /// The data grid column collection property
        /// </summary>
        public static readonly DependencyProperty DataGridColumnCollectionProperty = DependencyProperty.Register(
            "DataGridColumnCollection",
            typeof(DataGridColumnCollection),
            typeof(SelectCodeComponent),
            new PropertyMetadata(
                default(DataGridColumnCollection),
                (o, args) => (o as SelectCodeComponent)?.RefreshDataGridColumnCollection(args)));

        /// <summary>
        /// The header property
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(SelectCodeComponent), new PropertyMetadata(default(string), (o, args) => ((SelectCodeComponent)o).HeaderTextBlock.Text = args.NewValue.ToString()));

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectCodeComponent"/> class.
        /// </summary>
        public SelectCodeComponent() => this.InitializeComponent();

        /// <summary>
        /// Gets or sets the data grid column collection.
        /// </summary>
        public DataGridColumnCollection DataGridColumnCollection
        {
            get => (DataGridColumnCollection)this.GetValue(DataGridColumnCollectionProperty);
            set => this.SetValue(DataGridColumnCollectionProperty, value);
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get => (string)this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Refreshes the items.
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected override void RefreshItems(IEnumerable args)
        {
            this.EnhancedDataGrid.ItemsSource = args;
        }

        /// <summary>
        /// Refreshes the data grid column collection.
        /// </summary>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void RefreshDataGridColumnCollection(DependencyPropertyChangedEventArgs args)
        {
            this.EnhancedDataGrid.SetColumns = args.NewValue as ObservableCollection<DataGridColumn>;
        }

        private void EnhancedDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.EnhancedDataGrid.SelectedItem is CodeBase item)
            {
                this.Text = item.Code;
                if (this.OpenDialogButton.CommandTarget is DialogHost dialogHost)
                {
                    dialogHost.IsOpen = false;
                }
            }
        }
    }
}