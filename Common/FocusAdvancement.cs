// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 12-24-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-28-2019
// ***********************************************************************
// <copyright file="FocusAdvancement.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Common
{
    using System.Windows;
    using System.Windows.Input;

    public static class FocusAdvancement
    {
        public static readonly DependencyProperty AdvancesByEnterKeyProperty = DependencyProperty.RegisterAttached(
            "AdvancesByEnterKey",
            typeof(bool),
            typeof(FocusAdvancement),
            new UIPropertyMetadata(OnAdvancesByEnterKeyPropertyChanged));

        public static bool GetAdvancesByEnterKey(DependencyObject obj)
        {
            return (bool)obj.GetValue(AdvancesByEnterKeyProperty);
        }

        public static void SetAdvancesByEnterKey(DependencyObject obj, bool value)
        {
            obj.SetValue(AdvancesByEnterKeyProperty, value);
        }

        private static void Keydown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter))
            {
                return;
            }

            var element = sender as UIElement;
            element?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private static void OnAdvancesByEnterKeyPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UIElement element))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                element.KeyDown += Keydown;
            }
            else
            {
                element.KeyDown -= Keydown;
            }
        }
    }
}