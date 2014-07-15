using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;
using User = LittlePlace.Api.ApiRequest.Commands.Result.User;

namespace LittlePlace.Api.ApiRequest.Commands.Users
{
    public class GetMyFriendsCommand : BaseCommand<Response<List<User>>>
    {

        public override string ActionName
        {
            get { return "getmyfriends"; }
        }

        public GetMyFriendsCommand(HttpClient httpClient)
            :base("user",httpClient,new Dictionary<string, string>())
        {
            this.IsCached = true;
        }

        public async override System.Threading.Tasks.Task<Response<List<User>>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var userResults=base.Deserialize(responseString);
            //получаем фоточки по урлу в бинари
            foreach (var user in userResults.Result)
            {
                if (!String.IsNullOrEmpty(user.PhotoUrl))
                {
                    user.PhotoRaw = await _restClient.GetByteArrayAsync(user.PhotoUrl);
                    user.RawPhotoString = Convert.ToBase64String(user.PhotoRaw);
                }
            }
            return userResults;

        }

        

      
    }
}
