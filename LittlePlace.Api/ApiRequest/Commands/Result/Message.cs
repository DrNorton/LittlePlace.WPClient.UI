using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class Message : INotifyPropertyChanged
    {
        private string _messageText;
        private string _login;
        private DateTime _sentTime;
        private int _eventId;

         [JsonProperty("MessageText")]
        public string MessageText
        {
            get { return _messageText; }
             set
             {
                 _messageText = value;
                 OnPropertyChanged("MessageText");
             }
        }
         [JsonProperty("Login")]
        public string Login
        {
            get { return _login; }
             set
             {
                 _login = value;
                 OnPropertyChanged("Login");
             }
        }
         [JsonProperty("SentTime")]
        public DateTime SentTime
        {
            get { return _sentTime; }
             set
             {
                 _sentTime = value;
                 OnPropertyChanged("SentTime");
             }
        }
         [JsonProperty("EventId")]
        public int EventId
        {
            get { return _eventId; }
             set
             {
                 _eventId = value;
                 OnPropertyChanged("EventId");
             }
        }

         public event PropertyChangedEventHandler PropertyChanged;

         protected void OnPropertyChanged(string name)
         {
             PropertyChangedEventHandler handler = PropertyChanged;
             if (handler != null)
             {
                 handler(this, new PropertyChangedEventArgs(name));
             }
         }
    }
}
