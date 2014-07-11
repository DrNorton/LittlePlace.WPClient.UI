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
    public class AddFriendToEventCommand : BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "addfriendtoevent"; }
        }

        public AddFriendToEventCommand(HttpClient httpClient,int eventId,int friendId)
            :base("events",httpClient)
        {
            FullUrl = String.Format("{0}&friendId={1}&eventId={2}", Url, eventId, friendId);
        }

        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var results = Response<string>.Deserialize(responseString);
            return results;
        }
    }
}
