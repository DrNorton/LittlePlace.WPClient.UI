using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class FriendPositionResult
    {
        [JsonProperty("FriendId")]
        public int FriendId { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }


        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

    }
}
