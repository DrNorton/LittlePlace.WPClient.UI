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
    public class GetMyOwnEventsCommand : BaseCommand<Response<List<Event>>>
    {

        public override string ActionName
        {
            get { return "getmyownevents"; }
        }

        public GetMyOwnEventsCommand(HttpClient httpClient)
            :base("events",httpClient)
        {
            this.IsCached = true;
        }

       
    }
}
