using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;
using User = LittlePlace.Api.ApiRequest.Commands.Result.User;

namespace LittlePlace.Api.ApiRequest.Commands.Users
{
    public class GetMeCommand : BaseCommand<Response<User>>
    {
        public override string ActionName
        {
            get { return "getme"; }
        }

        public GetMeCommand(HttpClient httpClient)
            :base("user",httpClient)
        {
            this.IsCached = true;
          
        }

        public async override System.Threading.Tasks.Task<Response<User>> Execute()
        {
            
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var user = base.Deserialize(responseString);
            //получаем фоточки по урлу в бинари
            //получаем фоточки по урлу в бинари
          
                if (!String.IsNullOrEmpty(user.Result.PhotoUrl))
                {
                    user.Result.PhotoRaw = await _restClient.GetByteArrayAsync(String.Format(user.Result.PhotoUrl+"?time{0}",DateTime.Now.Ticks));
                    user.Result.RawPhotoString = Convert.ToBase64String(user.Result.PhotoRaw);
                }
            
            
            return user;
        }
    }
}
