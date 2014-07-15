using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LittlePlace.Api;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MainViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;

        public MainViewModel(INavigationService navigationService,ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;

            AutorityIfNeeded();
         
        }

        private async void AutorityIfNeeded()
        {
            if (!_littlePlaceApiService.IsAuthorizated)
            {
                _navigationService.UriFor<AuthViewModel>().Navigate();
            }
        }


        public void MapTileTap()
        {
            _navigationService.UriFor<MapViewModel>().Navigate();
        }

        public void FriendTileTap()
        {
            _navigationService.UriFor<ContactsViewModel>().Navigate();
        }

        public void MessageTileTap()
        {
            _navigationService.UriFor<DialogsListViewModel>().Navigate();
        }

        public void SettingTileTap()
        {
            
        }

        protected override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
       
        }
    }
}
