using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Events
{
    public class GetMessagesFromEventCommand: BaseCommand<Response<List<Message>>>
    {
        public override string ActionName
        {
            get { return "getmessagesfromevent"; }
        }

        public GetMessagesFromEventCommand(HttpClient httpClient,int eventId)
            : base("message", httpClient)
        {
            FullUrl = String.Format("{0}&eventid={1}&cache={2}", Url, eventId,Guid.NewGuid().ToString());
        }

        
    }
}
