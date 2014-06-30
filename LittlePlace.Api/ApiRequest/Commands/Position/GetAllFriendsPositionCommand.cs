using System;
using System.Collections.Generic;
using System.Net.Http;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class GetAllFriendsPositionCommand : BaseCommand<Response<List<FriendPositionResult>>>
    {
        public override string ActionName
        {
            get { return "getallfriendsposition"; }
        }

        public GetAllFriendsPositionCommand(HttpClient httpClient)
            : base("position", httpClient)
        {
            this.IsCached = true;
        }

        public async override System.Threading.Tasks.Task<Response<List<FriendPositionResult>>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var userResults = Response<List<FriendPositionResult>>.Deserialize(responseString);
            return userResults;
        }
    }
}
