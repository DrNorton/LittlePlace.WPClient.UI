using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class RegisterResult
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }
    }
}
