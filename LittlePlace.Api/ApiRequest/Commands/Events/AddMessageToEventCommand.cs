using System;
using System.Collections.Generic;
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

        public AddMessageToEventCommand(HttpClient httpClient,Dictionary<string,string> dict)
            :base("message",httpClient,dict)
        {
           
        }

       
    }
}
