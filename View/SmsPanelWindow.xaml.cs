// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="SmsPanelWindow.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.View
{
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;

    using mshtml;

    using WaybillApp.ViewModel;

    /// <summary>
    /// Class SmsPanelWindow.
    /// Implements the <see cref="Window" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [ComVisible(true)]
    public partial class SmsPanelWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsPanelWindow"/> class.
        /// </summary>
        public SmsPanelWindow()
        {
            this.InitializeComponent();
            this.ProgressBar.Visibility = Visibility.Collapsed;
            this.Loaded += this.SmsPanelWindowLoaded;
        }

        /// <summary>
        /// Gets the panel view model.
        /// </summary>
        private SmsPanelViewModel PanelViewModel => this.DataContext as SmsPanelViewModel;

        /// <summary>
        /// Buttons the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.WebBrowser.Document is HTMLDocument document)
            {
                var message = document.getElementById("message");
                message.click();
                Thread.Sleep(500);
                var recipientsNumber = document.getElementById("recipients_number");
                var messageContent = document.getElementById("message_content");
                recipientsNumber.innerText = this.PanelViewModel.OutboundInvoiceViewModel.ReceiverPhoneNumber;
                messageContent.innerText = this.PanelViewModel.MessageContent.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Handles the OnClick event of the Refresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            this.WebBrowser.Navigate(App.SmsPanelIp);
        }

        /// <summary>
        /// SMSs the panel window loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SmsPanelWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.ProgressBar.Visibility = Visibility.Visible;
            this.WebBrowser.Visibility = Visibility.Collapsed;
            Directory.GetCurrentDirectory();
            this.WebBrowser.Navigate("http://192.168.8.1/html/smsinbox.html?smsinbox");
            this.WebBrowser.LoadCompleted += (ss, ee) =>
                {
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                    this.WebBrowser.Visibility = Visibility.Visible;
                    if (this.PanelViewModel.OutboundInvoiceViewModel != null)
                    {
                        this.Massage.Visibility = Visibility.Visible;
                    }
                };
        }
    }
}