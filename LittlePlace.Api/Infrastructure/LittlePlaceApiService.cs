using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Auth;
using LittlePlace.Api.ApiRequest.Commands.Position;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.ApiRequest.Commands.Users;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.Infrastructure
{
    public interface ILittlePlaceApiService
    {
        Task<Response<string>>  Logon();
        Task<Response<string>> Logoff();
        Task<Response<RegisterResult>> Register();
        Task<Response<string>> AddMyPosition();
        Task<Response<List<UserPosition>>> GetFriendPosition();
        Task<Response<List<UserResult>>> GetMyFriends();
        Task<Response<List<FriendPositionResult>>> GetAllFriendsPosition();
    }

    public class LittlePlaceApiService : ILittlePlaceApiService
    {
        private readonly IExecuterService _executerService;
        private HttpClient _httpClient;
        private CookieContainer _cookies;

        public LittlePlaceApiService(IExecuterService executerService)
        {
            _executerService = executerService;
            _httpClient = GetHttpClient();
        }

        public async Task<Response<string>>  Logon()
        {
         
            var logonCommand = new LogonCommand(_httpClient,"DrNorton","rianon");
            return await _executerService.ExecuteCommand(logonCommand,false);
        }

        public async Task<Response<string>> Logoff()
        {

            var logoffCommand = new LogoffCommand(_httpClient);
            return await _executerService.ExecuteCommand(logoffCommand, false);
        }

        public async Task<Response<RegisterResult>> Register()
        {
            var registerCommand = new RegisterCommand(_httpClient, "DrNorton", "rianon");
            return await _executerService.ExecuteCommand(registerCommand,false);
        }

        public async Task<Response<List<UserResult>>> GetMyFriends()
        {
            var getMyFriendsCommand = new GetMyFriendsCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<List<FriendPositionResult>>> GetAllFriendsPosition()
        {
            var getMyFriendsCommand = new GetAllFriendsPositionCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<string>> AddMyPosition()
        {
            var addPosition = new AddMyPositionCommand(_httpClient,12,12);
           return await _executerService.ExecuteCommand(addPosition,false);
        }

        public async Task<Response<List<UserPosition>>> GetFriendPosition()
        {
            var getFriendPosition = new GetFriendPositionCommand(_httpClient,3);
            return await _executerService.ExecuteCommand(getFriendPosition,false);
        }
       

        private HttpClient GetHttpClient()
        {
           
            HttpClientHandler handler = new HttpClientHandler();
            _cookies=handler.CookieContainer;
            handler.UseCookies = true;
         
           var client= new HttpClient(handler);
            return client;
        }
    }
}
