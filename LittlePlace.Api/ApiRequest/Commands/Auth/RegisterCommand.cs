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
            FullUrl = String.Format("{0}/{1}?login={2}&pass={3}", Url, ActionName, login, pass);
        }

        public async override System.Threading.Tasks.Task<Response<RegisterResult>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            return Response<RegisterResult>.Deserialize(responseString);
        }

      

       
    }
}
