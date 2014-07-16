using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class GetFriendPositionCommand : BaseCommand<Response<List<UserPosition>>>
    {
       

        public override string ActionName
        {
            get { return "getfriendposition"; }
        }

        public GetFriendPositionCommand(HttpClient restClient,Dictionary<string,string> dict )
            :base("position",restClient,dict)
        {
            
        }

       
    }
}
