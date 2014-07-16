using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class AddMyPositionCommand:BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "addmyposition"; }
        }

        public AddMyPositionCommand(HttpClient restClient,Dictionary<string,string> dict )
            :base("position",restClient,dict)
        {
            
        }
       
    }
}
