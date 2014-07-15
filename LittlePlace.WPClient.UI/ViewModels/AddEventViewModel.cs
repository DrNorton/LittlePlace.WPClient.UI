using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Converterts;
using LittlePlace.WPClient.UI.Extensions;
using LittlePlace.WPClient.UI.Services.Base;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Microsoft.Phone.Tasks;
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
        private byte[] _newPhoto;

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
            _newEvent.RawPhotoString = GetDefaultImage();
            _newEvent.EventTime = DateTime.Now;
            _navigationService.Navigating += _navigationService_Navigating;
            base.StartDataLoading();
              
        }

        private string GetDefaultImage()
        {
            Uri uri = new Uri("/Content/noPhoto.png", UriKind.Relative);
            BitmapImage bi = new BitmapImage(uri);
            bi.CreateOptions = BitmapCreateOptions.None;
            var bytes=Helpers.ImageToBytes(bi);
            return System.Convert.ToBase64String(bytes);
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

        public void ChangePhoto()
        {
            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto != null)
            {
                using (BinaryReader br = new BinaryReader(e.ChosenPhoto))
                {
                    _newPhoto = br.ReadBytes((int)e.ChosenPhoto.Length);
                }
                _newEvent.RawPhotoString = System.Convert.ToBase64String(_newPhoto);
            }
        }

        public async void AddEvent()
        {
            var me = (await _littlePlaceApiService.GetMe());
            _newEvent.OwnerId = me.Result.UserId;
            if (_newPhoto != null)
            {
                var res = (await _littlePlaceApiService.UploadAvatar(_newPhoto)).Result;
                _newEvent.ImageUrl = res;
            }
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
