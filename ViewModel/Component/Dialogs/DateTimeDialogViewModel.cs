// ***********************************************************************
// Assembly         : WaybillApp
// Author           : Artan
// Created          : 01-20-2020
//
// Last Modified By : Artan
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="DateTimeDialogViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel.Component.Dialogs
{
    using System;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using WaybillApp.View.Dialogs;

    public class DateTimeDialogViewModel : BindableBase, IDialogAware
    {
        private DateTime selectedDateTime = DateTime.Now;

        public DateTimeDialogViewModel(IDialogService dialogService)
        {
            this.DialogService = dialogService;
        }

        public event Action<IDialogResult> RequestClose;

        public string Title { get; }

        /// <summary>
        /// Gets the dialog service.
        /// </summary>
        public IDialogService DialogService { get; }

        public DateTime SelectedDateTime
        {
            get => this.selectedDateTime;
            set => this.SetProperty(ref this.selectedDateTime, value);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            var parameters = new DialogParameters
                                 {
                                     { "SelectedDateTime", SelectedDateTime.ToString("O") },
                                 };
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters?.GetValue<DateTime?>("SelectedDateTime") is DateTime dateTime)
            {
                this.SelectedDateTime = dateTime;
            }
        }
    }
}