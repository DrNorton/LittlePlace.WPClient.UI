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

        public GetFriendPositionCommand(HttpClient restClient,int friendId)
            :base("position",restClient)
        {
            FullUrl = String.Format("{0}/{1}?friendId={2}", Url, ActionName, friendId);
        }

        public async override System.Threading.Tasks.Task<Response<List<UserPosition>>> Execute()
        {
      
            var responseString = await _restClient.GetStringAsync(FullUrl);
            return Response<List<UserPosition>>.Deserialize(responseString);
        }

       
    }
}
