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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ActionName
        {
            get { return "addmyposition"; }
        }

        public AddMyPositionCommand(HttpClient restClient)
            :base("position",restClient)
        {
            
        }

        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
            var url = String.Format("{0}/{1}?latitude={2}&longitude={3}", Url, ActionName, Latitude, Longitude);
            var responseString = await _restClient.GetStringAsync(url);
            return Response<string>.Deserialize(responseString);
        }

        public override string BuildCacheKey()
        {
            throw new NotImplementedException();
        }
    }
}
