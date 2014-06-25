using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.Models
{
    public class UserPosition
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
