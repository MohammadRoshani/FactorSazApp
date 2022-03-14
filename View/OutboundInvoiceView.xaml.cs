// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="OutboundInvoiceView.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.View
{
    using System.Windows;
    using System.Windows.Controls;

    using WaybillApp.ViewModel.OutboundInvoice;

    /// <summary>
    /// Class OutboundInvoiceView.
    /// Implements the <see cref="UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class OutboundInvoiceView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundInvoiceView"/> class.
        /// </summary>
        public OutboundInvoiceView()
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
            ((OutboundInvoiceViewModel)this.DataContext).Save();
        }

        /// <summary>
        /// Handles the OnClick event of the ButtonBase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((OutboundInvoiceViewModel)this.DataContext).AddNewInvoice();
        }
    }
}