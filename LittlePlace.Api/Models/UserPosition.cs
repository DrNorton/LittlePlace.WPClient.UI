using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.Models
{
    public class UserPosition
    {
        private double _latitude;
        private double _longitude;
        private DateTime _time;
        private string _description;

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

        [JsonProperty("Time")]
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        [JsonProperty("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
