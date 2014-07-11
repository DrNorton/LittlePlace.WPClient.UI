﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Events
{
    public class GetMyOwnEventsCommand : BaseCommand<Response<List<EventResult>>>
    {

        public override string ActionName
        {
            get { return "getmyownevents"; }
        }

        public GetMyOwnEventsCommand(HttpClient httpClient)
            :base("events",httpClient)
        {
            
        }

        public async override System.Threading.Tasks.Task<Response<List<EventResult>>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var results = Response<List<EventResult>>.Deserialize(responseString);
            return results;
        }
    }
}
