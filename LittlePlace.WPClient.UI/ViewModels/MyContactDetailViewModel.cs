using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.ViewModels.Auth;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Microsoft.Phone.Tasks;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class MyContactDetailViewModel:LoadingScreen
    {
        private readonly INavigationService _navigationService;
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private User _profile;
        private byte[] _newPhoto;

        public MyContactDetailViewModel(INavigationService navigationService,ILittlePlaceApiService littlePlaceApiService)
        {
            _navigationService = navigationService;
            _littlePlaceApiService = littlePlaceApiService;
            base.ProgressIndicatorStatus(true);
            base.StartDataLoading();
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
                    _newPhoto = br.ReadBytes((int) e.ChosenPhoto.Length);
                }
                Profile.RawPhotoString = System.Convert.ToBase64String(_newPhoto);
            }
        } 

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Profile =(await _littlePlaceApiService.GetMe()).Result;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            base.ProgressIndicatorStatus(false);
        }

        public async void SaveChanges()
        {
            base.ProgressIndicatorStatus(true);
            if (_newPhoto != null)
            {
                var res=(await _littlePlaceApiService.UploadAvatar(_newPhoto)).Result;
                Profile.PhotoUrl = res;
            }
           
            await _littlePlaceApiService.UpdateMe(Profile);
            base.ProgressIndicatorStatus(false);
        }

        public void NavigateToChangePassword()
        {
            _navigationService.UriFor<ChangePasswordViewModel>().Navigate();
        }
    }
}
