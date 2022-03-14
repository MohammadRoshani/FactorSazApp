// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-20-2020
//
// Last Modified By : Artan
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="PopupDialogService.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Services
{
    using System;
    using System.Windows;

    using MaterialDesignThemes.Wpf;

    using Prism.Ioc;
    using Prism.Services.Dialogs;

    /// <summary>
    /// Class PopupDialogService.
    /// Implements the <see cref="Prism.Services.Dialogs.IDialogService" />
    /// </summary>
    public class PopupDialogService : IDialogService
    {
        private readonly IContainerExtension containerExtension;

        public PopupDialogService(IContainerExtension containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback) =>
            this.ShowDialogInternal(name, parameters, callback);

        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback) =>
            this.ShowDialogInternal(name, parameters, callback);

        /// <summary>
        ///     Configures the content of the dialog window.
        /// </summary>
        /// <param name="dialogName">Name of the dialog.</param>
        /// <returns>System.ValueTuple&lt;System.Object, IDialogAware&gt;.</returns>
        /// <exception cref="NullReferenceException">
        ///     A dialog's content must be a FrameworkElement
        ///     or
        ///     A dialog's ViewModel must implement the IDialogAware interface
        /// </exception>
        protected virtual (object, IDialogAware) ConfigureDialogWindowContent(string dialogName)
        {
            var content = this.containerExtension.Resolve<object>(dialogName);
            if (!(content is FrameworkElement dialogContent))
            {
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            }

            if (!(dialogContent.DataContext is IDialogAware viewModel))
            {
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");
            }

            return (content, viewModel);
        }

        /// <summary>
        /// Shows the dialog internal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="callback">The callback.</param>
        private void ShowDialogInternal(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            var (content, dialogAware) = this.ConfigureDialogWindowContent(name);

            DialogHost.Show(
                content,
                (sender, args) =>
                    {
                        dialogAware.RequestClose += callback;
                        dialogAware.OnDialogOpened(parameters);
                    },
                (sender, args) =>
                    {
                        if (!dialogAware.CanCloseDialog())
                        {
                            args.Cancel();
                        }

                        dialogAware.OnDialogClosed();
                        dialogAware.RequestClose -= callback;
                    });
        }
    }
}