// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-02-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="InputComponent.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System.Windows;
    using System.Windows.Controls;

    using MaterialDesignThemes.Wpf;

    /// <summary>
    /// Class InputComponent.
    /// Implements the <see cref="UserControl" />
    /// </summary>
    public class InputComponent : UserControl
    {
        /// <summary>
        /// The label property
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(InputComponent), new PropertyMetadata(default(string)));

        /// <summary>
        /// The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(InputComponent), new PropertyMetadata(default(string)));

        /// <summary>
        /// The kind property
        /// </summary>
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register("Kind", typeof(PackIconKind), typeof(InputComponent), new PropertyMetadata(default(PackIconKind)));

        /// <summary>
        /// The content text alignment property
        /// </summary>
        public static readonly DependencyProperty ContentTextAlignmentProperty = DependencyProperty.Register("ContentTextAlignment", typeof(TextAlignment), typeof(InputComponent), new PropertyMetadata(default(TextAlignment)));

        /// <summary>
        /// The content minimum width property
        /// </summary>
        public static readonly DependencyProperty ContentMinWidthProperty = DependencyProperty.Register("ContentMinWidth", typeof(int), typeof(InputComponent), new PropertyMetadata(150));

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label
        {
            get => (string)this.GetValue(LabelProperty);
            set => this.SetValue(LabelProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public PackIconKind Kind
        {
            get => (PackIconKind)this.GetValue(KindProperty);
            set => this.SetValue(KindProperty, value);
        }

        /// <summary>
        /// Gets or sets the content text alignment.
        /// </summary>
        public TextAlignment ContentTextAlignment
        {
            get => (TextAlignment)this.GetValue(ContentTextAlignmentProperty);
            set => this.SetValue(ContentTextAlignmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum width of the content.
        /// </summary>
        /// <value>The minimum width of the content.</value>
        public int ContentMinWidth
        {
            get => (int)this.GetValue(ContentMinWidthProperty);
            set => this.SetValue(ContentMinWidthProperty, value);
        }
    }
}
