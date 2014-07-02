using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class AuthViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private string _password;
        private string _login;

        public AuthViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                base.NotifyOfPropertyChange(() => CanLogon);
                base.NotifyOfPropertyChange(()=>Password);
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                base.NotifyOfPropertyChange(() => CanLogon);
                base.NotifyOfPropertyChange(()=>Login);
            }
        }

        public async void Logon()
        {
            var logonResult=await _littlePlaceApiService.Logon(Login, Password);
            if (logonResult.ErrorCode != 0)
            {
                MessageBox.Show(logonResult.ErrorMessage);
            }
            else
            {
                _navigationService.GoBack();
            }
        }

        public bool CanLogon
        {
            get
            {
                if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password))
                {
                    return true;
                }
                return false; 
            }
        }

        protected override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
