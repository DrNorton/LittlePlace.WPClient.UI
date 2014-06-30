using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class FriendContactDetailViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private User _profile;
        public int UserId { get; set; }

        public FriendContactDetailViewModel(INavigationService navigationService, ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
       
        }

        protected override void OnViewReady(object view)
        {
            base.ProgressIndicatorStatus(true);
            base.StartDataLoading();
            base.OnViewReady(view);
        }

        public User Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                base.NotifyOfPropertyChange(() => Profile);
            }
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Profile = (await _littlePlaceApiService.GetUserByUserId(UserId)).Result;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            base.ProgressIndicatorStatus(false);
        }
    }
}
