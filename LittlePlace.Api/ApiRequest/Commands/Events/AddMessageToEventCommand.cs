using System;
using System.Net.Http;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Events
{
    public class AddMessageToEventCommand : BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "addmessagetoevent"; }
        }

        public AddMessageToEventCommand(HttpClient httpClient,string message,int eventId)
            :base("message",httpClient)
        {
            FullUrl = String.Format("{0}&message={1}&eventid={2}&cache={3}", Url, message, eventId, Guid.NewGuid().ToString());
        }

       
    }
}
