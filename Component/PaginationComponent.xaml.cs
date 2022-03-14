// ***********************************************************************
// Assembly         : WaybillApp
// Author           : M.Roshani
// Created          : 01-01-2020
//
// Last Modified By : M.Roshani
// Last Modified On : 01-01-2020
// ***********************************************************************
// <copyright file="PaginationComponent.xaml.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Component
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using Prism.Commands;

    /// <summary>
    /// Class PaginationComponent.
    /// Implements the <see cref="UserControl" />
    /// Implements the <see cref="INotifyPropertyChanged" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    public partial class PaginationComponent : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// The page size property
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(PaginationComponent), new PropertyMetadata(10, OnPageSizeChange));

        /// <summary>
        /// The items count property
        /// </summary>
        public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register("ItemsCount", typeof(long), typeof(PaginationComponent), new PropertyMetadata(1L, OnItemsCountChange));

        /// <summary>
        /// The selected page index property
        /// </summary>
        public static readonly DependencyProperty SelectedPageIndexProperty = DependencyProperty.Register("SelectedPageIndex", typeof(long), typeof(PaginationComponent), new PropertyMetadata(default(long)));

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationComponent" /> class.
        /// </summary>
        public PaginationComponent()
        {
            this.InitializeComponent();
            this.cbNumberOfRecords.ItemsSource = new[] { 10, 20, 50, 100, 500, 1000 };
            this.NavigateCommand = new DelegateCommand<string>(this.NavigateClick);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the update page action.
        /// </summary>
        public event Action UpdatePageAction = delegate { };

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; }

        /// <summary>
        /// Gets or sets the items count.
        /// </summary>
        public long ItemsCount
        {
            get => (long)this.GetValue(ItemsCountProperty);
            set => this.SetValue(ItemsCountProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get => (int)this.GetValue(PageSizeProperty);
            set => this.SetValue(PageSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the index of the selected page.
        /// </summary>
        /// <value>The index of the selected page.</value>
        public long SelectedPageIndex
        {
            get => (long)this.GetValue(SelectedPageIndexProperty);
            set => this.SetValue(SelectedPageIndexProperty, value);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is enable first.
        /// </summary>
        /// <value><c>true</c> if this instance is enable first; otherwise, <c>false</c>.</value>
        public bool IsEnableFirst => this.SelectedPageIndex > 0;

        /// <summary>
        /// Gets a value indicating whether this instance is enable previous.
        /// </summary>
        /// <value><c>true</c> if this instance is enable previous; otherwise, <c>false</c>.</value>
        public bool IsEnablePrevious => this.SelectedPageIndex >= 1;

        /// <summary>
        /// Gets a value indicating whether this instance is enable next.
        /// </summary>
        /// <value><c>true</c> if this instance is enable next; otherwise, <c>false</c>.</value>
        public bool IsEnableNext => this.ItemsCount - (this.SelectedPageIndex * this.PageSize) > this.PageSize;

        /// <summary>
        /// Gets a value indicating whether this instance is enable last.
        /// </summary>
        /// <value><c>true</c> if this instance is enable last; otherwise, <c>false</c>.</value>
        public bool IsEnableLast => this.IsEnableNext || this.SelectedPageIndex == this.PageCounts;

        /// <summary>
        /// Gets the page counts.
        /// </summary>
        public long PageCounts => (this.ItemsCount / this.PageSize) + (this.ItemsCount % this.PageSize != 0 ? 1 : 0);

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label => $"{this.SelectedPageIndex + 1} / {this.PageCounts}";

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            this.UpdatePageAction?.Invoke();
            this.InvalidateVisibility();
        }

        /// <summary>
        /// Handles the <see cref="E:PageSizeChange" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnPageSizeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PaginationComponent paginationComponent)
            {
                paginationComponent.SelectedPageIndex = 0;
                paginationComponent.Refresh();
                paginationComponent.RisePropertyChanged(nameof(paginationComponent.PageCounts));
            }
        }

        /// <summary>
        /// Handles the <see cref="E:ItemsCountChange" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnItemsCountChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PaginationComponent paginationComponent)
            {
                paginationComponent.RisePropertyChanged(nameof(paginationComponent.PageCounts));
                paginationComponent.RisePropertyChanged(nameof(paginationComponent.Label));
                paginationComponent.InvalidateVisibility();
            }
        }

        /// <summary>
        /// Navigates the click.
        /// </summary>
        /// <param name="mode">The mode.</param>
        private void NavigateClick(string mode)
        {
            switch (mode)
            {
                case "First":
                    this.SelectedPageIndex = 0;
                    break;
                case "Previous":
                    this.SelectedPageIndex = Math.Max(0, this.SelectedPageIndex - 1);
                    break;
                case "Next":
                    this.SelectedPageIndex = this.SelectedPageIndex + 1;
                    break;
                case "Last":
                    this.SelectedPageIndex = this.PageCounts - 1;
                    break;
            }

            this.Refresh();
        }

        /// <summary>
        /// Invalidates the visibility.
        /// </summary>
        private void InvalidateVisibility()
        {
            this.RisePropertyChanged(nameof(this.IsEnableFirst));
            this.RisePropertyChanged(nameof(this.IsEnablePrevious));
            this.RisePropertyChanged(nameof(this.IsEnableNext));
            this.RisePropertyChanged(nameof(this.IsEnableLast));
            this.RisePropertyChanged(nameof(this.Label));
        }

        private void OnLoaded(object sender, RoutedEventArgs e) => this.InvalidateVisibility();

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RisePropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}