using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class EventMember
    {
        private int _id;
        private int _memberId;
        private int _eventId;

        [JsonProperty("Id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

       [JsonProperty("MemberId")]
        public int MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

       [JsonProperty("EventId")]
        public int EventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }
    }
}
