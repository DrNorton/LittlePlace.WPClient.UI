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
    public class GetByUserIdCommand : BaseCommand<Response<User>>
    {

        public override string ActionName
        {
            get { return "getbyid"; }
        }


        public GetByUserIdCommand(HttpClient httpClient,int userId)
            :base("user",httpClient)
        {
            this.IsCached = true;
            FullUrl = String.Format("{0}&friendId={1}", Url, userId);
        }

        public async override System.Threading.Tasks.Task<Response<User>> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var user = base.Deserialize(responseString);
            //получаем фоточки по урлу в бинари
                if (!String.IsNullOrEmpty(user.Result.PhotoUrl))
                {
                    user.Result.PhotoRaw = await _restClient.GetByteArrayAsync(user.Result.PhotoUrl);
                    user.Result.RawPhotoString = Convert.ToBase64String(user.Result.PhotoRaw);
                }
            return user;
        }
    }
}
