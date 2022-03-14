// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="InboundInvoiceView.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.View
{
    using System.Windows;

    using WaybillApp.ViewModel.InboundInvoice;

    /// <summary>
    /// Class InboundInvoiceView.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class InboundInvoiceView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InboundInvoiceView"/> class.
        /// </summary>
        public InboundInvoiceView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Buttons the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            ((InboundInvoiceViewModel)this.DataContext).Save();
        }

        /// <summary>
        /// Handles the OnClick event of the Print control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}