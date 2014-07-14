using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class EventViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private Event _showingEvent;

        public int EventId { get; set; }

       

        public EventViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
        }

        protected override void OnViewReady(object view)
        {
            base.StartDataLoading();
            base.OnViewReady(view);
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ShowingEvent=(await _littlePlaceApiService.GetMyOwnEventsCommand()).Result.FirstOrDefault(x => x.EventId == EventId);
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           
        }

        public Event ShowingEvent
        {
            get { return _showingEvent; }
            set
            {
                _showingEvent = value;
                base.NotifyOfPropertyChange(() => ShowingEvent);
            }
        }
    }
}
