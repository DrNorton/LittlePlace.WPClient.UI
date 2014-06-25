using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
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
           Get();
        }

        private async void Get()
        {
            var logResult = await _littlePlaceApiService.Logon();
            var friends =await _littlePlaceApiService.GetMyFriends();
            var ds = await _littlePlaceApiService.GetFriendPosition();
        }

        public void MapTileTap()
        {
            _navigationService.UriFor<MapViewModel>().Navigate();
        }

        public void FriendTileTap()
        {
            _navigationService.UriFor<FriendsListViewModel>().Navigate();
        }

        public void ProfileTileTap()
        {
            
        }

        public void SettingTileTap()
        {
            
        }
    }
}
