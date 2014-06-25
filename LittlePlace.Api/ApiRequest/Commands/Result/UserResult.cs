using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class UserResult
    {
        [JsonProperty("FriendId")]
        public int FriendId { get; set; }

        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Photo")]
        public string PhotoUrl { get; set; }


        [JsonIgnore]
        public byte[] PhotoRaw { get; set; }

        [JsonProperty("RawPhotoString")]
        public string RawPhotoString { get; set; }
    }
}
