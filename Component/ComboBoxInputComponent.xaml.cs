// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-07-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ComboBoxInputComponent.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    /// <summary>
    /// Class ComboBoxInputComponent.
    /// Implements the <see cref="InputWithItemsComponent{int}" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    public partial class ComboBoxInputComponent : InputWithItemsComponent<int>
    {
        public ComboBoxInputComponent() => this.InitializeComponent();
    }
}
