using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.EventMessages;
using LittlePlace.WPClient.UI.EventMessages.Maps;
using LittlePlace.WPClient.UI.Models.MapModels;
using LittlePlace.WPClient.UI.ViewModels.Base;
using LittlePlace.WPClient.UI.Views;
using Yandex.Maps;
using Yandex.Positioning;


namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MapViewModel:LoadingScreen,IHandle<NewPositionEventMessage>
    {
        //Сервисы
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
       //Пушпины
        private List<FriendPushpin> _friendPushpins;
        private MePushpin _mePushpin;
        //DataContainers
        private List<User> _friends;
        //Всякая шляпа

        private Visibility _friendWindowTipVisibility = Visibility.Collapsed;
        private IEventAggregator _eventAggregator;
        private GeoCoordinate _centerPoint = new GeoCoordinate(55.7522200, 37.6155600);
        private Map _map;
        private ObservableCollection<EventPushpin> _ownEvents;


        public MapViewModel(INavigationService navigationService, ILittlePlaceApiService littlePlaceApiService,IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _navigationService.Navigating += _navigationService_Navigating;
            _ownEvents=new ObservableCollection<EventPushpin>();
        }

        void _navigationService_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (_friendWindowTipVisibility == Visibility.Visible)
                {
                    FriendWindowTipVisibility=Visibility.Collapsed;
                    e.Cancel = true;
                }
            }
        }

        protected override void OnViewLoaded(object view)
        {
            var vi = view as MapView;
            _map = vi.Map;
            base.OnViewLoaded(view);
        }

        public void FriendShowPanel()
        {
            FriendWindowTipVisibility = Visibility.Visible;
        }

        public void NavigateToFriendPosition(User selectedUser)
        {
            var position=_friendPushpins.FirstOrDefault(x => x.User == selectedUser).Position;
            _eventAggregator.Publish(new NavigateMapToPushpinMessage() { Position = position });
            FriendWindowTipVisibility = Visibility.Collapsed;
        }

        public void NavigateToFriendView(int userId)
        {
            _navigationService.UriFor<FriendContactDetailViewModel>().WithParam(x=>x.UserId,userId).Navigate();
        }

        public void NavigateToAddEventView(GestureEventArgs args)
        {
            Point position = args.GetPosition(_map);
            GeoCoordinate coordinates = _map.ViewportPointToCoordinates(new Yandex.Media.Point(position.X, position.Y));
            _navigationService.UriFor<AddEventViewModel>().WithParam(x=>x.Latitude,coordinates.Latitude).WithParam(x=>x.Longitude,coordinates.Longitude).Navigate();
        }

        public void Handle(NewPositionEventMessage message)
        {
            MePushpin=new MePushpin(){ContentVisibility = Visibility.Visible,IsNotifying = true,Position = new GeoCoordinate(message.Latitude,message.Longitude),State = PushPinState.Collapsed,ZIndex = 1};
        }

        private List<FriendPushpin> CreateFriendPositionList(IEnumerable<FriendPositionResult> positions, IEnumerable<User> friends)
        {
            var friendPositionList = new List<FriendPushpin>();
            foreach (var friendPosition in positions)
            {
                var friend = friends.FirstOrDefault(x => x.UserId == friendPosition.FriendId);
                friendPositionList.Add(new FriendPushpin(friend, friendPosition));
            }
            return friendPositionList;
        }

        protected async override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            var positions = (await _littlePlaceApiService.GetAllFriendsPosition());
            Friends = (await _littlePlaceApiService.GetMyFriends()).Result;
            FriendPushpins = CreateFriendPositionList(positions.Result, Friends);
            var events = (await _littlePlaceApiService.GetMyOwnEventsCommand()).Result;
            CreateEventPusphins(events);

        }

        private void CreateEventPusphins(IEnumerable<Event> events)
        {
            foreach (var ev in events)
            {
                OwnEvents.Add(new EventPushpin(ev));
            }
            base.NotifyOfPropertyChange(()=>OwnEvents);
        }

        public void NavigateToEventsView()
        {
            _navigationService.UriFor<EventsListViewModel>().Navigate();
        }

        protected override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
          
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }

        public List<FriendPushpin> FriendPushpins
        {
            get { return _friendPushpins; }
            set
            {
                _friendPushpins = value;
                base.NotifyOfPropertyChange(() => FriendPushpins);
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

        public Visibility FriendWindowTipVisibility
        {
            get { return _friendWindowTipVisibility; }
            set
            {
                _friendWindowTipVisibility = value;
                base.NotifyOfPropertyChange(() => FriendWindowTipVisibility);
            }
        }

        public MePushpin MePushpin
        {
            get { return _mePushpin; }
            set
            {
                _mePushpin = value;
                base.NotifyOfPropertyChange(()=>MePushpin);
            }
        }

        public GeoCoordinate CenterPoint
        {
            get { return _centerPoint; }
            set
            {
                _centerPoint = value;
                base.NotifyOfPropertyChange(()=>CenterPoint);
            }
        }

        public ObservableCollection<EventPushpin> OwnEvents
        {
            get { return _ownEvents; }
            set
            {
                _ownEvents = value;
                base.NotifyOfPropertyChange(()=>OwnEvents);
            }
        }
    }
}
