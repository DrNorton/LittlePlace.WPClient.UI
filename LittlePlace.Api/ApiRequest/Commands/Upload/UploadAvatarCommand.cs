using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Upload
{
    public class UploadAvatarCommand : BaseCommand<Response<string>>
    {
        public byte[] Image { get; set; }

        public override string ActionName
        {
            get { return "addavatar"; }
        }

        public UploadAvatarCommand(HttpClient httpclient)
            :base("upload",httpclient)
        {
          
        }

        public async override System.Threading.Tasks.Task<Response<string>> Execute()
        {
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(Image);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "\"thumbnail\"", "\"1403844990224.jpg\"");
            using (var response = await _restClient.PostAsync(FullUrl, requestContent))
            {
                 var responseString = await response.Content.ReadAsStringAsync();
                 var result = Response<string>.Deserialize(responseString);
                return result;
            }
            
        }
    }
}
