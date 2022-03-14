// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="OutboundInvoiceListView.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.View
{
    using WaybillApp.ViewModel.OutboundInvoice;

    /// <summary>
    /// Class OutboundInvoiceListView.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class OutboundInvoiceListView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoiceListView"/> class.
        /// </summary>
        public OutboundInvoiceListView() => this.InitializeComponent();

        private void OnUpdatePageAction() => (this.DataContext as OutboundInvoiceListViewModel)?.UpdatePageCommand.Execute();
    }
}