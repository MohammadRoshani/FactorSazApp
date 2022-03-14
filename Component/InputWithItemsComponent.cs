// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-07-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="InputWithItemsComponent.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;

    public class InputWithItemsComponent<T> : InputComponent
    {
        /// <summary>
        /// The items property
        /// </summary>
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items",
            typeof(IEnumerable<T>),
            typeof(InputWithItemsComponent<T>),
            new PropertyMetadata(
                default(IEnumerable<T>),
                (o, args) => (o as InputWithItemsComponent<T>)?.RefreshItems(args.NewValue as IEnumerable)));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(T),
            typeof(InputWithItemsComponent<T>),
            new PropertyMetadata(default(T)));

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<T> Items
        {
            get => (IEnumerable<T>)this.GetValue(ItemsProperty);
            set => this.SetValue(ItemsProperty, value);
        }

        public T SelectedItem
        {
            get => (T)this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Refreshes the items.
        /// </summary>
        /// <param name="newItems">The new items.</param>
        protected virtual void RefreshItems(IEnumerable newItems)
        {
        }
    }
}