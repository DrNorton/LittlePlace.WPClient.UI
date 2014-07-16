using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.Models;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class DialogsListViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private List<FullDialog> _privateMessages;

        public DialogsListViewModel(ILittlePlaceApiService littlePlaceApiService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _privateMessages=new List<FullDialog>();
            base.StartDataLoading();
        }

       

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var dialogs = (await _littlePlaceApiService.GetMyDialogs()).Result;
            CreateFullDialog(dialogs);
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
          
           
        }

        private async void CreateFullDialog(List<Dialog> dialogs)
        {
            var friends=(await _littlePlaceApiService.GetMyFriends()).Result;
            foreach (var dialog in dialogs)
            {
                _privateMessages.Add(new FullDialog(dialog,friends));
            }
            
        }

        public List<FullDialog> PrivateMessages
        {
            get { return _privateMessages; }
            set
            {
                _privateMessages = value;
                base.NotifyOfPropertyChange(() => PrivateMessages);
            }
        }
    }
}
