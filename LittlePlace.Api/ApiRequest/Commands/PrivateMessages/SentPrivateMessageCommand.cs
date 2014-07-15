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
    public class SentPrivateMessageCommand : BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { throw new NotImplementedException(); }
        }

        public SentPrivateMessageCommand(HttpClient httpClient,int toId,string message)
            :base("message",httpClient)
        {
            FullUrl = String.Format("{0}&message={1}&toid={2}", Url, message, toId);
        }

      
    }
}
