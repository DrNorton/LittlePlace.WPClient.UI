using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class SingleNewsViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private NewsResult _singleNews;
        public int NewsId { get; set; }

        public NewsResult SingleNews
        {
            get { return _singleNews; }
            set
            {
                _singleNews = value;
                base.NotifyOfPropertyChange(()=>SingleNews);
            }
        }

        public SingleNewsViewModel(ILittlePlaceApiService littlePlaceApiService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            base.ProgressIndicatorStatus(true);
            base.StartDataLoading();
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SingleNews =(await _littlePlaceApiService.GetNewsById(NewsId)).Result;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            base.ProgressIndicatorStatus(false);
        }
    }
}
