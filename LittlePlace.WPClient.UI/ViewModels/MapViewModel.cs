using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.EventMessages;
using LittlePlace.WPClient.UI.Maps.Models;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Platform;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MapViewModel:LoadingScreen,IHandle<NewPositionEventMessage>
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly ApplicationIdCredentialsProvider _credentials;
        private readonly IEventAggregator _eventAggregator;
        private List<FriendPositionResult> _friendsLocations;
        private MapPushpinModel _centerMap;
        private ObservableCollection<MapPushpinModel> _myPositions;
        private int _zoomLevel = 5;


        public MapViewModel(INavigationService navigationService, ILittlePlaceApiService littlePlaceApiService, ApplicationIdCredentialsProvider credentials,IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
            _credentials = credentials;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _centerMap = new MapPushpinModel();
            _centerMap.Position.Latitude = 37.609218;
            _centerMap.Position.Longitude = 55.753559;
            MyPositions = new ObservableCollection<MapPushpinModel>();
        }

        public List<FriendPositionResult> FriendsLocations
        {
            get { return _friendsLocations; }
            set
            {
                _friendsLocations = value;
                base.NotifyOfPropertyChange(()=>FriendsLocations);
            }
        }

        public MapPushpinModel CenterMap
        {
            get { return _centerMap; }
            set
            {
                _centerMap = value;
                base.NotifyOfPropertyChange(()=>CenterMap);
            }
        }

        public ApplicationIdCredentialsProvider Credentials
        {
            get { return _credentials; }
        }

        public int ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                if (value >= 0)
                {
                    _zoomLevel = value;
                }

                base.NotifyOfPropertyChange(() => ZoomLevel);
            }
        }

        public void ZoomOut()
        {
            ZoomLevel--;
        }

        public void ZoomIn()
        {
            ZoomLevel++;
        }

        public void CenterMapOnMe()
        {
            CenterMap = MyPositions.FirstOrDefault();
        }

        public ObservableCollection<MapPushpinModel> MyPositions
        {
            get { return _myPositions; }
            set
            {
                _myPositions = value;
                base.NotifyOfPropertyChange(()=>MyPositions);
            }
        }

        protected async override void OnViewReady(object view)
        {
           base.OnViewReady(view);
           var res=await _littlePlaceApiService.Logon("DrNorton","rianon");
           var result = await _littlePlaceApiService.GetAllFriendsPosition();
           FriendsLocations = result.Result;

        }

        public void Handle(NewPositionEventMessage message)
        {
            var location = new GeoCoordinate(){Latitude = message.Latitude,Longitude = message.Longitude};
            var mapPushpinModel = new MapPushpinModel(){Position = location,Time = DateTime.Now,ImagePath = new BitmapImage(new Uri(@"/Content\Icons\red-circle.png",UriKind.Relative))};
            _myPositions.Add(mapPushpinModel);
            CenterMap = mapPushpinModel;

          base.NotifyOfPropertyChange(() => MyPositions);
        }

        protected override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
          
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }
    }
}
