using System;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LittlePlace.Api.ApiRequest.Commands.Auth
{
    public class LogonCommand:BaseCommand<Response<string>>
    {
        public string Login { get; set; }
        public string Pass { get; set; }

        public override string ActionName
        {
            get { return "logon"; }
        }

        public LogonCommand(HttpClient restClient)
            :base("auth",restClient)
        {
            
        }

        public async override Task<Response<string>> Execute()
        {
            var httpClient = new HttpClient();
            var url = String.Format("{0}/{1}?login={2}&pass={3}", Url, ActionName, Login, Pass);
            var responseString = await _restClient.GetStringAsync(url);
            return Response<string>.Deserialize(responseString);
        }

        public override string BuildCacheKey()
        {
            throw new NotImplementedException();
        }

       
    }
}
