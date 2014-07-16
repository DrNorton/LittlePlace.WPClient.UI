using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Auth
{
    public class RegisterCommand:BaseCommand<Response<RegisterResult>>
    {
        public override string ActionName
        {
            get { return "register"; }
        }

        public RegisterCommand(HttpClient restClient,Dictionary<string,string> dict )
            : base("auth",restClient,dict)
        {
          
        }
       
    }
}
