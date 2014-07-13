using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Extensions;
using LittlePlace.WPClient.UI.Services.Base;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Yandex.Maps.Geocoding.Dto;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class AddEventViewModel:LoadingScreen
    {
        private readonly IGeoCodingService _geoCodingService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private Event _newEvent;
        private List<User> _friends;
        private bool _showFriendsAdd;
        private List<User> _selectedFriends; 

        public double Latitude { get; set; }
        public double Longitude { get; set; }

      

  

        public AddEventViewModel(IGeoCodingService geoCodingService,ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _geoCodingService = geoCodingService;
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
            _geoCodingService.Completed = Completed;
            _geoCodingService.Failed = Failed;
            _newEvent=new Event();
            _newEvent.EventTime = DateTime.Now;
            _navigationService.Navigating += _navigationService_Navigating;
            base.StartDataLoading();
              
        }

        void _navigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (ShowFriendsAdd  == true)
                {
                    ShowFriendsAdd = false;
                    e.Cancel = true;
                }
            }
        }

        

        private void Failed(Exception obj)
        {
            
        }

        private void Completed(GeocodeObject obj)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => _newEvent.Address = obj.Title);
        }

      
        public async void AddEvent()
        {
            var me = (await _littlePlaceApiService.GetMe());
            _newEvent.OwnerId = me.Result.UserId;
           var newInsertedEvent=(await  _littlePlaceApiService.AddEvent(_newEvent)).Result;
            _newEvent.EventId = newInsertedEvent.EventId;
           await _littlePlaceApiService.AddFriendsToEvent(_newEvent, SelectedFriends);
            _navigationService.GoBack();
        }

        public void ShowFriendSelector()
        {
            ShowFriendsAdd = true;
        }
   

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _newEvent.Latitude = Latitude;
            _newEvent.Longitude = Longitude;
           _geoCodingService.GetAddress(Latitude,Longitude);
           Friends = (await _littlePlaceApiService.GetMyFriends()).Result;
            SelectedFriends = Friends;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           
        }

        public Event NewEvent
        {
            get { return _newEvent; }
            set
            {
                _newEvent = value;
                base.NotifyOfPropertyChange(() => NewEvent);
            }
        }

        public List<User> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                base.NotifyOfPropertyChange(() => Friends);
            }
        }

        public bool ShowFriendsAdd
        {
            get { return _showFriendsAdd; }
            set
            {
                _showFriendsAdd = value;
                base.NotifyOfPropertyChange(()=>ShowFriendsAdd);
            }
        }

        public List<User> SelectedFriends
        {
            get { return _selectedFriends; }
            set
            {
                _selectedFriends = value;
                base.NotifyOfPropertyChange(()=>SelectedFriends);
            }
        }
    }

  
}
