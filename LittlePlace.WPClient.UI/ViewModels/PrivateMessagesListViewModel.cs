using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.Api.Models;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class PrivateMessagesListViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private Response<List<PrivateMessage>> _privateMessages;

        public PrivateMessagesListViewModel(ILittlePlaceApiService littlePlaceApiService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            base.StartDataLoading();
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           _privateMessages=await _littlePlaceApiService.GetMyPrivateMessages();
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           
        }
    }
}
