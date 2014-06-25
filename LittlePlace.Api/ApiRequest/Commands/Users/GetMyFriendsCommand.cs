using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Users
{
    public class GetMyFriendsCommand : BaseCommand<Response<List<UserResult>>>
    {

        public override string ActionName
        {
            get { return "getmyfriends"; }
        }

        public GetMyFriendsCommand(HttpClient httpClient)
            :base("user",httpClient)
        {
            this.IsCached = true;
            FullUrl = String.Format("{0}/{1}", Url, ActionName);
        }

        public async override System.Threading.Tasks.Task<Response<List<UserResult>>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var userResults=Response<List<UserResult>>.Deserialize(responseString);
            //получаем фоточки по урлу в бинари
            foreach (var user in userResults.Result)
            {
               user.PhotoRaw =await _restClient.GetByteArrayAsync(user.PhotoUrl);
               user.RawPhotoString = Convert.ToBase64String(user.PhotoRaw);
            }
            return userResults;

        }

        

      
    }
}
