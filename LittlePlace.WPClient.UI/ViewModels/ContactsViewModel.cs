using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Extensions;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class ContactsViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private User _profile;

        private List<AlphaKeyGroup<User>> _groupedUsers = new List<AlphaKeyGroup<User>>();
        private List<User> _friends;

        public List<AlphaKeyGroup<User>> GroupedUsers
        {
            get
            {
                return _groupedUsers;
            }
            set
            {
                _groupedUsers = value;
                base.NotifyOfPropertyChange(() => GroupedUsers);
            }
        }

        public ContactsViewModel(INavigationService navigationService, ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
          
        }

        public void NavigateToMyProfileDetail()
        {
            _navigationService.UriFor<MyContactDetailViewModel>().Navigate();
        }

        public void NavigateToFriendContactView(User par)
        {
            _navigationService.UriFor<FriendContactDetailViewModel>().WithParam(x=>x.UserId,par.UserId).Navigate();
        }
      
        public User Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                base.NotifyOfPropertyChange(()=>Profile);
            }
        }

        public List<User> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                base.NotifyOfPropertyChange(()=>Friends);
            }
        }

        protected override void OnViewReady(object view)
        {
            base.ProgressIndicatorStatus(true);
            base.StartDataLoading();
            base.OnViewReady(view);
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Profile = (await _littlePlaceApiService.GetMe()).Result;
            Friends = (await _littlePlaceApiService.GetMyFriends()).Result;
            CreateGroupedList();
        }

        private void CreateGroupedList()
        {
            GroupedUsers = AlphaKeyGroup<User>.CreateGroups(
                        Friends,
                        CultureInfo.CurrentCulture,
                        (User s) => { return s.Login.ElementAt(0).ToString().ToLower(); },
                        true);
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            base.ProgressIndicatorStatus(false);
        }
    }
}
