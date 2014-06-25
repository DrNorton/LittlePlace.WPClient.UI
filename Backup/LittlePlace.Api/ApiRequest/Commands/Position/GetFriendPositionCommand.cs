using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class GetFriendPositionCommand:BaseCommand<Response<UserPosition>>
    {
        public int FriendId { get; set; }

        public override string ActionName
        {
            get { return "getfriendposition"; }
        }

        public GetFriendPositionCommand(HttpClient restClient)
            :base("position",restClient)
        {
            
        }

        public async override System.Threading.Tasks.Task<Response<UserPosition>> Execute()
        {
            var url = String.Format("{0}/{1}?friendId={2}&longitude={3}", Url, ActionName,FriendId);
            var responseString = await _restClient.GetStringAsync(url);
            return Response<UserPosition>.Deserialize(responseString);
        }

        public override string BuildCacheKey()
        {
            throw new NotImplementedException();
        }
    }
}
