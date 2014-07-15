using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class Dialog
    {
        private string _id;
        private List<DialogMember> _members;
        private List<PrivateMessage> _messages;

        [JsonProperty("_id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

       [JsonProperty("Messages")]
        public List<PrivateMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        [JsonProperty("Members")]
        public List<DialogMember> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        
    }
}
