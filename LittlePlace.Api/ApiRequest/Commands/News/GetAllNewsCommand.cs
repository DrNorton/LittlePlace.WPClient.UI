using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.News
{
    public class GetAllNewsCommand : BaseCommand<Response<List<NewsResult>>>
    {
        public override string ActionName
        {
            get { return "getallnews"; }
        }

        public GetAllNewsCommand(HttpClient httpClient)
            :base("news",httpClient,new Dictionary<string, string>())
        {
            this.IsCached = true;
        }

       
    }
}
