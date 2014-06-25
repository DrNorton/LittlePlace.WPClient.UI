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
       
        public string Login { get; set; }
        public string Pass { get; set; }

        public override string ActionName
        {
            get { return "register"; }
        }

        public RegisterCommand(HttpClient restClient)
            : base("auth",restClient)
        {
           
        }

        public async override System.Threading.Tasks.Task<Response<RegisterResult>> Execute()
        {
         
            var url = String.Format("{0}/{1}?login={2}&pass={3}", Url, ActionName, Login, Pass);
            var responseString = await _restClient.GetStringAsync(url);
            return Response<RegisterResult>.Deserialize(responseString);
        }

        public override string BuildCacheKey()
        {
            throw new NotImplementedException();
        }

       
    }
}
