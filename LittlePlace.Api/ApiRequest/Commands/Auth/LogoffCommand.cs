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
            
        }
       

       

      
    }
}
