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
        private List<Event> _invitedEvents;
        private Event _selectedFriendEvent; 

        public EventsListViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
            base.StartDataLoading();
        }

        protected override void OnViewReady(object view)
        {
            SelectedFriendEvent = null;
            SelectedEvent = null;
            base.OnViewReady(view);
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
                if (value != null)
                {
                    NavigateToEventView(value,true);
                }
                base.NotifyOfPropertyChange(()=>SelectedEvent);
            }
        }

        public List<Event> InvitedEvents
        {
            get { return _invitedEvents; }
            set
            {
                _invitedEvents = value;
                base.NotifyOfPropertyChange(()=>InvitedEvents);
            }
        }

        public Event SelectedFriendEvent
        {
            get { return _selectedFriendEvent; }
            set
            {
                _selectedFriendEvent = value;
                if (value != null)
                {
                    NavigateToEventView(value,false);
                }
               
                base.NotifyOfPropertyChange(()=>SelectedFriendEvent);
            }
        }

        private void NavigateToEventView(Event value,bool isMyOrNot)
        {
            _navigationService.UriFor<EventViewModel>().WithParam(x=>x.EventId,value.EventId).WithParam(x=>x.IsMyOwnEvent,isMyOrNot).Navigate();
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MyEvents=(await _littlePlaceApiService.GetMyOwnEventsCommand()).Result;
            InvitedEvents = (await _littlePlaceApiService.GetMyInvitedEventsCommand()).Result;
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }
    }
}
