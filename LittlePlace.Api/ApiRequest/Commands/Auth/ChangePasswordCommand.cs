using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Auth
{
    public class ChangePasswordCommand : BaseCommand<Response<string>>
    {
        public ChangePasswordCommand(HttpClient restClient,string oldPass,string newPass)
            :base("auth",restClient)
        {
            FullUrl = String.Format("{0}&oldpass={1}&newpass={2}", Url, oldPass, newPass);
        }
        public override string ActionName
        {
            get { return "changepassword"; }
        }

       
    }
}
