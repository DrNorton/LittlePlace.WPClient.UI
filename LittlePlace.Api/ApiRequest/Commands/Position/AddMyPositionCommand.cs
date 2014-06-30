using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class AddMyPositionCommand:BaseCommand<Response<string>>
    {
        public override string ActionName
        {
            get { return "addmyposition"; }
        }

        public AddMyPositionCommand(HttpClient restClient,double latitude,double longitude)
            :base("position",restClient)
        {
            FullUrl = String.Format("{0}&latitude={1}&longitude={2}", Url,  latitude, longitude);
        }

        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
        
            var responseString = await _restClient.GetStringAsync(FullUrl);
            return Response<string>.Deserialize(responseString);
        }

       
    }
}
