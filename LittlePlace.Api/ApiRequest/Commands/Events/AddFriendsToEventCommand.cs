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

        public AddFriendsToEventCommand(HttpClient httpClient, int eventId, List<int> friends)
            :base("events",httpClient)
        {
            var str = "[";
            for(int i=0;i<friends.Count;i++)
            {
                str += friends[i];
                if (i != friends.Count - 1)
                {
                    str += ",";
                }
            }
            str+= "]";
            FullUrl = String.Format("{0}&friends={1}&eventId={2}", Url,str, eventId);
        }

        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var results = Response<string>.Deserialize(responseString);
            return results;
        }
    }
}
