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
            FullUrl = String.Format("{0}&login={1}&pass={2}", Url, login, pass);
        }

      

      
    }
}
