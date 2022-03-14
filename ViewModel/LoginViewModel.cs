// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="LoginViewModel.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.ViewModel
{
    using System;

    using Prism.Commands;
    using Prism.Mvvm;

    using WaybillApp.Model;

    /// <summary>
    /// Class LoginViewModel.
    /// Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class LoginViewModel : BindableBase
    {
        /// <summary>
        /// The active code
        /// </summary>
        private string activeCode;

        /// <summary>
        /// The login request
        /// </summary>
        private bool loginRequest;

        /// <summary>
        /// The password
        /// </summary>
        private string password;

        /// <summary>
        /// The show active box
        /// </summary>
        private bool showActiveBox;

        /// <summary>
        /// The user
        /// </summary>
        private readonly User user;

        /// <summary>
        /// The user name
        /// </summary>
        private string userName;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            this.LoginCommand = new DelegateCommand(
                () =>
                    {
                        if (this.UserName == "Add" && this.Password == "User")
                        {
                            this.ShowActiveBox = true;
                            this.UserName = null;
                            this.Password = null;
                        }

                        // if (string.IsNullOrEmpty(this.ActiveCode))
                        // {
                        // this.user = await UserValidation.GetValidUser(this.UserName, this.Password);
                        // this.LoginRequest = true;
                        // }
                        // else
                        // {
                        // if (await UserValidation.CreateUser(this.UserName, this.Password, this.ActiveCode))
                        // {
                        // this.UserName = null;
                        // this.Password = null;
                        // this.ActiveCode = null;
                        // this.ShowActiveBox = false;
                        // }
                        // }
                    });
        }

        /// <summary>
        /// Gets or sets the active code.
        /// </summary>
        public string ActiveCode
        {
            get => this.activeCode;
            set => this.SetProperty(ref this.activeCode, value);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="LoginViewModel"/> is expired.
        /// </summary>
        /// <value><c>true</c> if expired; otherwise, <c>false</c>.</value>
        public bool Expired => this.LoginRequest && DateTime.Now > this.user?.ExpiredDate;

        /// <summary>
        /// Gets a value indicating whether [invalid login].
        /// </summary>
        /// <value><c>true</c> if [invalid login]; otherwise, <c>false</c>.</value>
        public bool InvalidLogin =>
            this.LoginRequest && (this.user == null || this.UserName != this.user.Name
                                                    || this.Password != this.user.Password);

        /// <summary>
        /// Gets or sets the login command.
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [login request].
        /// </summary>
        /// <value><c>true</c> if [login request]; otherwise, <c>false</c>.</value>
        public bool LoginRequest
        {
            get => this.loginRequest;
            set
            {
                this.SetProperty(ref this.loginRequest, value);
                this.RaisePropertyChanged(nameof(this.InvalidLogin));
                this.RaisePropertyChanged(nameof(this.Expired));
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show active box].
        /// </summary>
        /// <value><c>true</c> if [show active box]; otherwise, <c>false</c>.</value>
        public bool ShowActiveBox
        {
            get => this.showActiveBox;
            set => this.SetProperty(ref this.showActiveBox, value);
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get => this.userName;
            set => this.SetProperty(ref this.userName, value);
        }
    }
}