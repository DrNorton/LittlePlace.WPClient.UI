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

        public RegisterCommand(HttpClient restClient,string login,string pass)
            : base("auth",restClient)
        {
            FullUrl = String.Format("{0}&login={1}&pass={2}", Url, login, pass);
        }

      

      

       
    }
}
