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
    public class EventsListViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private List<Event> _myEvents;
        private Event _selectedEvent;

        public EventsListViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
            base.StartDataLoading();
        }

        public List<Event> MyEvents
        {
            get { return _myEvents; }
            set
            {
                _myEvents = value;
                base.NotifyOfPropertyChange(()=>MyEvents);
            }
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                NavigateToEventView(value);
                base.NotifyOfPropertyChange(()=>SelectedEvent);
            }
        }

        private void NavigateToEventView(Event value)
        {
            _navigationService.UriFor<EventViewModel>().WithParam(x=>x.EventId,value.EventId).Navigate();
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MyEvents=(await _littlePlaceApiService.GetMyOwnEventsCommand()).Result;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }
    }
}
