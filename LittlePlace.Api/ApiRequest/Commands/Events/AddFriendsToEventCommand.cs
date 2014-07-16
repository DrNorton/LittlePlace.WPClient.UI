using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Events
{
    public class AddFriendsToEventCommand : BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "addfriendstoevent"; }
        }

        public AddFriendsToEventCommand(HttpClient httpClient, Dictionary<string,string> dict )
            :base("events",httpClient,dict)
        {
           
        }

       
    }
}
