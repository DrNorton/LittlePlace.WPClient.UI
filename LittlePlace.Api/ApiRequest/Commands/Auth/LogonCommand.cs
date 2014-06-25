using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LittlePlace.Api.ApiRequest.Commands.Auth
{
    public class LogonCommand:BaseCommand<Response<string>>
    {
      
        public override string ActionName
        {
            get { return "logon"; }
        }

        public LogonCommand(HttpClient restClient,string login,string pass)
            :base("auth",restClient)
        {
            FullUrl = String.Format("{0}/{1}?login={2}&pass={3}", Url, ActionName, login, pass);
        }

        public async override Task<Response<string>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            return Response<string>.Deserialize(responseString);
        }

      
    }
}
