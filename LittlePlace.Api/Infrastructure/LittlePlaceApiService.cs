using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Auth;
using LittlePlace.Api.ApiRequest.Commands.News;
using LittlePlace.Api.ApiRequest.Commands.Position;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.ApiRequest.Commands.Upload;
using LittlePlace.Api.ApiRequest.Commands.Users;
using LittlePlace.Api.Models;
using User = LittlePlace.Api.ApiRequest.Commands.Result.User;

namespace LittlePlace.Api.Infrastructure
{
    public interface ILittlePlaceApiService
    {
        bool IsAuthorizated { get; }
        Task<Response<string>>  Logon(string login,string pass);
        Task<Response<string>> Logoff();
        Task<Response<RegisterResult>> Register(string login,string pass);
        Task<Response<List<User>>> GetMyFriends();
        Task<Response<List<FriendPositionResult>>> GetAllFriendsPosition();
        Task<Response<string>> AddMyPosition(double latitude,double longitude);
        Task<Response<List<UserPosition>>> GetFriendPosition(int friendId);
        Task<Response<User>> GetMe();
        Task<Response<string>> UploadAvatar(byte[] image);
        Task<Response<string>> UpdateMe(User updatedUser);
        Task<Response<User>> GetUserByUserId(int userId);
        Task<Response<string>> ChangePasssword(string oldPass, string newPass);
        Task<Response<List<NewsResult>>> GetAllNews();
        Task<Response<NewsResult>> GetNewsById(int newsId);
    }

    public class LittlePlaceApiService : ILittlePlaceApiService
    {
        private readonly IExecuterService _executerService;
        private readonly ISettingService _settingService;
        private readonly ICacheService _cacheService;
        private HttpClient _httpClient;
        private CookieContainer _cookies;
        private bool _isAuthorizated;

        public LittlePlaceApiService(IExecuterService executerService,ISettingService settingService,ICacheService cacheService)
        {
            _executerService = executerService;
            _settingService = settingService;
            _cacheService = cacheService;
            GetCookies();
            _httpClient = GetHttpClient();
        }

        private void GetCookies()
        {
            _cookies = _settingService.AuthCookies;
        }

        public bool IsAuthorizated
        {
            get { return _cookies.Count != 0; }
        }

        public async Task<Response<string>>  Logon(string login,string pass)
        {
         
            var logonCommand = new LogonCommand(_httpClient,login,pass);
            var result= await _executerService.ExecuteCommand(logonCommand,false);
            _settingService.AuthCookies = _cookies;

            return result;
        }

        public async Task<Response<string>> Logoff()
        {

            var logoffCommand = new LogoffCommand(_httpClient);
            return await _executerService.ExecuteCommand(logoffCommand, false);
        }

        public async Task<Response<RegisterResult>> Register(string login,string pass)
        {
            var registerCommand = new RegisterCommand(_httpClient, login,pass);
            return await _executerService.ExecuteCommand(registerCommand,false);
        }

        public async Task<Response<string>> ChangePasssword(string oldPass, string newPass)
        {
            var registerCommand = new ChangePasswordCommand(_httpClient, oldPass, newPass);
            return await _executerService.ExecuteCommand(registerCommand, false);
        }

        public async Task<Response<List<User>>> GetMyFriends()
        {
            var getMyFriendsCommand = new GetMyFriendsCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<string>> UploadAvatar(byte[] image)
        {
            var uploadAvatarCommand = new UploadAvatarCommand(_httpClient);
            uploadAvatarCommand.Image = image;
            var result= await _executerService.ExecuteCommand(uploadAvatarCommand, false);
            _cacheService.RemoveCacheResult(new GetMeCommand(_httpClient));
            return result;
        }

        public async Task<Response<User>> GetMe()
        {
            var getMyFriendsCommand = new GetMeCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<User>> GetUserByUserId(int userId)
        {
            var getMyFriendsCommand = new GetByUserIdCommand(_httpClient,userId);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<string>> UpdateMe(User updatedUser)
        {
            var getMyFriendsCommand = new UpdateMeCommand(_httpClient,updatedUser);
            var result= await _executerService.ExecuteCommand(getMyFriendsCommand, false);
            _cacheService.RemoveCacheResult(new GetMeCommand(_httpClient));
            return result;
        }

        public async Task<Response<List<FriendPositionResult>>> GetAllFriendsPosition()
        {
            var getMyFriendsCommand = new GetAllFriendsPositionCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<List<NewsResult>>> GetAllNews()
        {
            var getMyFriendsCommand = new GetAllNewsCommand(_httpClient);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, false);
        }

        public async Task<Response<NewsResult>> GetNewsById(int newsId)
        {
            var getnewsById = new GetNewsByIdCommand(_httpClient,newsId);
            return await _executerService.ExecuteCommand(getnewsById, false);
        }


        public async Task<Response<string>> AddMyPosition(double latitude,double longitude)
        {
            var addPosition = new AddMyPositionCommand(_httpClient,latitude,longitude);
           return await _executerService.ExecuteCommand(addPosition,false);
        }

        public async Task<Response<List<UserPosition>>> GetFriendPosition(int friendId)
        {
            var getFriendPosition = new GetFriendPositionCommand(_httpClient,friendId);
            return await _executerService.ExecuteCommand(getFriendPosition,false);
        }

        private HttpClient GetHttpClient()
        {
           
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = _cookies;
            handler.UseCookies = true;
         
           var client= new HttpClient(handler);
            return client;
        }
    }
}
