using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class EventResult
    {
        private int _eventId;
        private string _name;
        private DateTime _created;
        private DateTime _eventTime;
        private double _latitude;
        private double _longitude;
        private int _ownerId;
        private string _address;
        private string _description;

         [JsonProperty("EventId")]
        public int EventId
        {
            get { return _eventId; }
             set
             {
                 _eventId = value;
                
             }
        }

         [JsonProperty("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [JsonProperty("Created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [JsonProperty("EventTime")]
        public DateTime EventTime
        {
            get { return _eventTime; }
            set { _eventTime = value; }
        }

        [JsonProperty("Latitude")]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        [JsonProperty("Longitude")]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        [JsonProperty("OwnerId")]
        public int OwnerId
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }

        [JsonProperty("Address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [JsonProperty("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
