using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class FriendsListViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private List<UserResult> _friends;

        public FriendsListViewModel(INavigationService navigationService,ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
        }

        public List<UserResult> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                base.NotifyOfPropertyChange(()=>Friends);
            }
        }

        protected async override void OnViewReady(object view)
        {
            base.ProgressIndicatorStatus(true);
            var log = (await _littlePlaceApiService.Logon());
            var result=(await _littlePlaceApiService.GetMyFriends());
            if (result.Result != null)
            {
                Friends = result.Result;
            }
            base.ProgressIndicatorStatus(false);
            base.OnViewReady(view);
        }

    }
}
