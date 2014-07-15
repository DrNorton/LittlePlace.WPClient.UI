using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Users
{
    public class UpdateMeCommand: BaseCommand<Response<string>>
    {

        public override string ActionName
        {
            get { return "updateme"; }
        }

        public UpdateMeCommand(HttpClient httpClient,Dictionary<string,string> dict)  
            : base("user", httpClient,dict)
        {

        }
    }
}
