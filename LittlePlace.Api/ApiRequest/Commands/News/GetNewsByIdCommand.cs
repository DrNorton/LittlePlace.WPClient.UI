﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.News
{
    public class GetNewsByIdCommand : BaseCommand<Response<NewsResult>>
    {
        public override string ActionName
        {
            get { return "getbyid"; }
        }

        public GetNewsByIdCommand(HttpClient httpClient,int newsId)
            :base("news",httpClient,new Dictionary<string, string>())
        {
            FullUrl = String.Format("{0}&newsId={1}", Url, newsId);
        }

       
    }
}
