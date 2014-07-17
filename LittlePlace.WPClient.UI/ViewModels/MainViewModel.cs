using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using LittlePlace.Api;
using LittlePlace.Api.ApiRequest.Commands.PrivateMessages;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.ApiRequest.Commands.Users;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.Extensions;
using LittlePlace.WPClient.UI.Models;
using LittlePlace.WPClient.UI.Models.DialogModels;
using LittlePlace.WPClient.UI.ViewModels.Auth;
using LittlePlace.WPClient.UI.ViewModels.Base;
using LittlePlace.WPClient.UI.Views;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MainViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ICacheService _cacheService;
        private readonly ICommandProvider _commandProvider;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private List<BitmapImage> _friendTileImages;
        private List<User> _friends;
        private ContactForTileModel _contactForTileModel=new ContactForTileModel(){ContactName = "Andrew",Text = "Te"};
        

        private int _count = 0;
        private ObservableCollection<ExtendedDialog> _dialogs;

        public MainViewModel(INavigationService navigationService,ICacheService cacheService,ICommandProvider commandProvider,ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _cacheService = cacheService;
            _commandProvider = commandProvider;
            _littlePlaceApiService = littlePlaceApiService;
            _dialogs = new ObservableCollection<ExtendedDialog>();
            AutorityIfNeeded();
        }

        protected async override void OnViewReady(object view)
        {
            var vs=view as MainView;
            base.OnViewReady(view);
            var api = IoC.Get<ILittlePlaceApiService>();
            if (api.IsAuthorizated)
            {
                GetFriendsFromCacheAndSetTileImages(vs);
                await CreateDialogList();
            }
        }

        private async Task CreateDialogList()
        {
            var cachedResult =
                _cacheService.GetCachedResult(_commandProvider.GetCommand<GetMyDialogsCommand>(new HttpClient(),
                    new Dictionary<string, string>()));
            if (cachedResult != null)
            {
                var dialogs = cachedResult.Result;
                foreach (var dialog in dialogs)
                {
                    var me = (await _littlePlaceApiService.GetMe()).Result;
                    Dialogs.Add(ExtendedDialog.CreateExtendedDialog(Friends, me, dialog));
                  
                    base.NotifyOfPropertyChange(()=>ContactForTileModel);
                   
                }
            }
        }

        private void GetFriendsFromCacheAndSetTileImages(MainView vs)
        {
            var cachedResult =
                _cacheService.GetCachedResult(_commandProvider.GetCommand<GetMyFriendsCommand>(new HttpClient(),
                    new Dictionary<string, string>()));
            if (cachedResult != null)
            {
                Friends = cachedResult.Result;
                vs.ContactImageTile.CreateImageSource = CreateImageSource;
            }
        }


        public ImageSource CreateImageSource(object uri)
        {
            if (Friends != null)
            {
                string rawPhotoString = Friends[_count].RawPhotoString;
                if (!String.IsNullOrEmpty(rawPhotoString))
                {
                    var currentImage = Convert.FromBase64String(rawPhotoString);
                    _count++;
                    if (_count == 4)
                    {
                        _count = 0;
                    }
                    return Helpers.BytesToImage(currentImage);
                }
            }
            return null;
        }

        public List<BitmapImage> FriendTileImages
        {
            get { return _friendTileImages; }
            set
            {
                _friendTileImages = value;
                base.NotifyOfPropertyChange(()=>FriendTileImages);
            }
        }

        public ObservableCollection<ExtendedDialog> Dialogs
        {
            get { return _dialogs; }
            set
            {
                _dialogs = value;
                base.NotifyOfPropertyChange(()=>Dialogs);
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

        public ContactForTileModel ContactForTileModel
        {
            get { return _contactForTileModel; }
            set
            {
                _contactForTileModel = value;
                base.NotifyOfPropertyChange(()=>ContactForTileModel);
            }
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

        public void ContactTileTap()
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
