using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.PrivateMessages
{
    public class SentPrivateMessageCommand : BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "sentprivatemessage"; }
        }

        public SentPrivateMessageCommand(HttpClient restClient,Dictionary<string,string> dict)
            : base("message", restClient, dict)
        {
            
        }
    }
}
