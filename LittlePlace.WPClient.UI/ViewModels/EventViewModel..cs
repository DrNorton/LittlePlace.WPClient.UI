using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;
using LittlePlace.WPClient.UI.Views;
using Message = LittlePlace.Api.ApiRequest.Commands.Result.Message;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class EventViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private Event _showingEvent;
        private List<Message> _messages;
        private string _message;
        private ListBox _messageListBox;
        private ObservableCollection<User> _participants;


        public int EventId { get; set; }
        public bool IsMyOwnEvent { get; set; }

       

        public EventViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
            _participants=new ObservableCollection<User>();
            
        }

        protected override void OnViewReady(object view)
        {
            base.StartDataLoading();
            base.OnViewReady(view);
            var vw = view as EventView;
            _messageListBox=vw.MessagesListBox;
        }

        private void NavigateToItemInListBox(Message message)
        {
            if (message != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    _messageListBox.ScrollIntoView(message);
                });
            }
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<Event> result;
            if (IsMyOwnEvent)
            {
                result = (await _littlePlaceApiService.GetMyOwnEventsCommand()).Result;
            }
            else
            {
                result = (await _littlePlaceApiService.GetMyInvitedEventsCommand()).Result;
            }
          
            if (result != null)
            {
                ShowingEvent = result.FirstOrDefault(x => x.EventId == EventId);
                await GetMessages();
                await GetFriendsFromEvent();
            }
            
        }

        private async Task GetFriendsFromEvent()
        {
            var eventMembers=(await _littlePlaceApiService.GetFriendsFromEvent(ShowingEvent)).Result;
            if (eventMembers != null)
            {
                var friends = (await _littlePlaceApiService.GetMyFriends()).Result;
                foreach (var member in eventMembers)
                {
                    User first = null;
                    foreach (User x in friends)
                    {
                        if (x.UserId == member.MemberId)
                        {
                            first = x;
                            break;
                        }
                    }
                    if(first!=null)
                        Deployment.Current.Dispatcher.BeginInvoke(() => Participants.Add(first));
                 
                }
            }
           
        }

        private async Task GetMessages()
        {
            Messages = (await _littlePlaceApiService.GetMessagesFromEventCommand(ShowingEvent)).Result;
            NavigateToItemInListBox(Messages.LastOrDefault());
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            
        }

        public async void SentMessage()
        {
            var result=await _littlePlaceApiService.AddMessageToEvent(Message, ShowingEvent);
            if (result.ErrorCode == 0)
            {
                await GetMessages();
            }
            Message = "";
            NavigateToItemInListBox(Messages.LastOrDefault());
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

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                base.NotifyOfPropertyChange(() => Message);
            }
        }


        public List<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                base.NotifyOfPropertyChange(()=>Messages);
            }
        }

        public ObservableCollection<User> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                base.NotifyOfPropertyChange(()=>Participants);
            }
        }
    }
}
