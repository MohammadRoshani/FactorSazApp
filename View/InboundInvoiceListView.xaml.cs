// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 10-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-15-2019
// ***********************************************************************
// <copyright file="InboundInvoiceListView.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.View
{
    using System.Windows.Controls;

    using WaybillApp.ViewModel.InboundInvoice;

    /// <summary>
    /// Interaction logic for InboundInvoiceListView.xaml
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class InboundInvoiceListView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InboundInvoiceListView" /> class.
        /// </summary>
        public InboundInvoiceListView() => this.InitializeComponent();

        private void OnUpdatePageAction() => (this.DataContext as InboundInvoiceListViewModel)?.UpdatePageCommand.Execute();
    }
}