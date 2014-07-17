using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels.Auth
{
    public class ChangePasswordViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;

        private string _oldPass;
        private string _newPass;

        public ChangePasswordViewModel(INavigationService navigationService,ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
           
        }

        public string OldPass
        {
            get { return _oldPass; }
            set
            {
                _oldPass = value;
                base.NotifyOfPropertyChange(()=>OldPass);
            }
        }

        public string NewPass
        {
            get { return _newPass; }
            set
            {
                _newPass = value;
                base.NotifyOfPropertyChange(()=>NewPass);
            }
        }

        protected override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           return;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }

        public async void SaveNewPassword()
        {
           var result=await  _littlePlaceApiService.ChangePasssword(OldPass, NewPass);
            if (result.ErrorCode == 0)
            {
                MessageBox.Show("Пароль изменен успешно");
                _navigationService.GoBack();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }
    }
}
