using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class PrivateMessage : INotifyPropertyChanged
    {
        private string _messageText;
        private int _fromId;
        private int _toId;
        private DateTime _sentTime;
        private string _id;

        [JsonProperty("_id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        [JsonProperty("FromId")]
        public int FromId
        {
            get { return _fromId; }
            set
            {
                _fromId = value;
                OnPropertyChanged("FromId");
            }
        }

        [JsonProperty("ToId")]
        public int ToId
        {
            get { return _toId; }
            set
            {
                _toId = value;
                OnPropertyChanged("ToId");
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
