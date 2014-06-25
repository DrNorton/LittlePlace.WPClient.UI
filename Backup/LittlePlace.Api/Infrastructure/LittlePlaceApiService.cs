using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Auth;
using LittlePlace.Api.ApiRequest.Commands.Position;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.Infrastructure
{
    public class LittlePlaceApiService
    {
        private HttpClient _httpClient;

        public LittlePlaceApiService()
        {
          _httpClient = GetHttpClient();
        }

        public async Task<Response<string>>  Logon()
        {
         
            var logonCommand = new LogonCommand(_httpClient);
            logonCommand.Login = "Dr.Norton";
            logonCommand.Pass = "rianon";
            return await logonCommand.Execute();
        }

        public async Task<Response<string>> Logoff()
        {

            var logonCommand = new LogoffCommand(_httpClient);
            return await logonCommand.Execute();
        }

        public async Task<Response<RegisterResult>> Register()
        {
            var registerCommand = new RegisterCommand(_httpClient);
            registerCommand.Login = "Dr.Norton";
            registerCommand.Pass = "rianon";
            return await registerCommand.Execute();
        }

        public async Task<Response<string>> AddMyPosition()
        {
            var addPosition = new AddMyPositionCommand(_httpClient);
            addPosition.Latitude = 12;
            addPosition.Longitude = 15;
            return await addPosition.Execute();
        }

        public async Task<Response<UserPosition>> GetFriendPosition()
        {
            var addPosition = new GetFriendPositionCommand(_httpClient);
            addPosition.FriendId = 3;
            return await addPosition.Execute();
        }
       

        private HttpClient GetHttpClient()
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            return new HttpClient(handler);
        }
    }
}
