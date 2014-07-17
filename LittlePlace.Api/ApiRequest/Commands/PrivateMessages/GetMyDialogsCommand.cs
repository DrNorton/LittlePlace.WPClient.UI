using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.PrivateMessages
{
    public class GetMyDialogsCommand : BaseCommand<Response<List<Dialog>>>
    {
        public override string ActionName
        {
            get { return "getmydialogs"; }
        }



        public GetMyDialogsCommand(HttpClient restClient,Dictionary<string,string> dict )
            :base("message",restClient,dict)
        {
            
        }
    }
}
