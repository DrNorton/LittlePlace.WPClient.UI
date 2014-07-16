using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class DialogMember
    {
        private int _memberId;

        [JsonProperty("MemberId")]
        public int MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

      
    }
}
