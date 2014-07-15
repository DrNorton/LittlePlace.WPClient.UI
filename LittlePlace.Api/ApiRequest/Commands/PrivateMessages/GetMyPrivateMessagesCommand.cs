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
    public class GetMyPrivateMessagesCommand : BaseCommand<Response<List<PrivateMessage>>>
    {
        public override string ActionName
        {
            get { return "getmyprivatemessages"; }
        }

        public GetMyPrivateMessagesCommand(HttpClient httpClient)
            : base("message", httpClient)
        {
            FullUrl = String.Format("{0}", Url);
        }

        
    }
}
