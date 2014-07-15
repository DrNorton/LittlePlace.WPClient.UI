using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class Event:  INotifyPropertyChanged
    {
        private int _eventId;
        private string _name;
        private DateTime _created;
        private DateTime _eventTime=DateTime.Now;
        private double _latitude;
        private double _longitude;
        private int _ownerId;
        private string _address;
        private string _description;
        private string _imageUrl;
        private string _rawPhotoString;

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

         [JsonProperty("Name")]
        public string Name
        {
            get { return _name; }
             set
             {
                 _name = value;
                 OnPropertyChanged("Name");
             }
        }

        [JsonProperty("Created")]
        public DateTime Created
        {
            get { return _created; }
            set
            {
                _created = value;
                OnPropertyChanged("Created");
            }
        }

        [JsonProperty("EventTime")]
        public DateTime EventTime
        {
            get { return _eventTime; }
            set
            {
                _eventTime = value;
                OnPropertyChanged("EventTime");
            }
        }

        [JsonProperty("Latitude")]
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                 OnPropertyChanged("Latitude");
            }
        }

        [JsonProperty("Longitude")]
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        [JsonProperty("OwnerId")]
        public int OwnerId
        {
            get { return _ownerId; }
            set
            {
                _ownerId = value;
                OnPropertyChanged("OwnerId");
            }
        }

        [JsonProperty("Address")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        [JsonProperty("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        [JsonProperty("ImageUrl")]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        [JsonIgnore]
        public byte[] PhotoRaw { get; set; }

        [JsonProperty("RawPhotoString")]
        public string RawPhotoString
        {
            get { return _rawPhotoString; }
            set
            {
                _rawPhotoString = value;
                OnPropertyChanged("RawPhotoString");
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
