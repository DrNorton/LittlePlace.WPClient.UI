using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.EventMessages;
using LittlePlace.WPClient.UI.Models.MapModels;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Yandex.Positioning;


namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MapViewModel:LoadingScreen,IHandle<NewPositionEventMessage>
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
  
        private readonly IEventAggregator _eventAggregator;
        private List<FriendPositionResult> _friendsLocations;
        private GeoCoordinate _centerMap;
        private List<FriendPushpin> _friendPushpins;


        public MapViewModel(INavigationService navigationService, ILittlePlaceApiService littlePlaceApiService,IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ConfigureCenterMap();

        }

        private void ConfigureCenterMap()
        {
           CenterMap=new GeoCoordinate();
           CenterMap.Latitude = 53.905153;
           CenterMap.Longitude = 27.558230;
        }

        public GeoCoordinate CenterMap
        {
            get { return _centerMap; }
            set
            {
                _centerMap = value;
                base.NotifyOfPropertyChange(()=>CenterMap);
            }
        }

        public List<FriendPushpin> FriendPushpins
        {
            get { return _friendPushpins; }
            set
            {
                _friendPushpins = value;
                base.NotifyOfPropertyChange(()=>FriendPushpins);
            }
        }


        protected async override void OnViewReady(object view)
        {
           base.OnViewReady(view);
           var positions = (await _littlePlaceApiService.GetAllFriendsPosition());
           var friends = (await _littlePlaceApiService.GetMyFriends()).Result;
           FriendPushpins=CreateFriendPositionList(positions.Result,friends);
        }

        private List<FriendPushpin> CreateFriendPositionList(IEnumerable<FriendPositionResult> positions, IEnumerable<User> friends)
        {
            var friendPositionList = new List<FriendPushpin>();
            foreach (var friendPosition in positions)
            {
                var friend=friends.FirstOrDefault(x => x.UserId == friendPosition.FriendId);
                friendPositionList.Add(new FriendPushpin(friend,friendPosition));
            }
            return friendPositionList;
        }

        public void Handle(NewPositionEventMessage message)
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
