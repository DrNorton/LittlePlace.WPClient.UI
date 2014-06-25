using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Auth
{
    public class LogoffCommand:BaseCommand<Response<string>>
    {

        public override string ActionName
        {
            get { return "logoff"; }
        }

        public LogoffCommand(HttpClient restClient)
            : base("auth",restClient)
        {
            FullUrl = String.Format("{0}/{1}", Url, ActionName);
        }
        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
            
           
            var responseString = await _restClient.GetStringAsync(FullUrl);
            return Response<string>.Deserialize(responseString);
        }

       

      
    }
}
