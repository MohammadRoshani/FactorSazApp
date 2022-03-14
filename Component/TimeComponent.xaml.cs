// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-16-2020
//
// Last Modified By : Artan
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="TimeComponent.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Prism.Services.Dialogs;

    using WaybillApp.View.Dialogs;
    using WaybillApp.ViewModel.Component.Dialogs;

    /// <summary>
    /// Class TimeComponent.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    public partial class TimeComponent : UserControl
    {
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(DateTime), typeof(TimeComponent), new PropertyMetadata(DateTime.Now));

        public TimeComponent() => this.InitializeComponent();

        public DateTime Time
        {
            get => (DateTime)this.GetValue(TimeProperty);
            set => this.SetValue(TimeProperty, value);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            (this.DataContext as DateTimeDialogViewModel)?.DialogService.ShowDialog(
                nameof(TimeDialog),
                new DialogParameters { { "SelectedDateTime", this.Time } },
                result =>
                    {
                        if (result.Parameters?.ContainsKey("SelectedDateTime") == true)
                        {
                            this.Time = result.Parameters.GetValue<DateTime>("SelectedDateTime");
                        }
                    });
        }
    }
}
