using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Auth;
using LittlePlace.Api.ApiRequest.Commands.Events;
using LittlePlace.Api.ApiRequest.Commands.News;
using LittlePlace.Api.ApiRequest.Commands.Position;
using LittlePlace.Api.ApiRequest.Commands.PrivateMessages;
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
        Task<Response<List<Event>>> GetMyOwnEventsCommand();
        Task<Response<List<Event>>> GetMyInvitedEventsCommand();
        Task<Response<string>> AddFriendToEvent(Event ev, User friend);
        Task<Response<Event>> AddEvent(Event ev);
        Task<Response<string>> AddFriendsToEvent(Event ev, IEnumerable<User> friends);
        Task<Response<string>> AddMessageToEvent(string message,Event ev);
        Task<Response<List<Message>>> GetMessagesFromEventCommand(Event ev);
        Task<Response<List<EventMember>>> GetFriendsFromEvent(Event ev);
        Task<Response<string>> UploadEventImage(byte[] image);
        Task<Response<string>> SentMessageToFriend(string message, int friendId);
        Task<Response<List<PrivateMessage>>> GetMyPrivateMessages();
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
            var r = _cookies.GetCookies(new Uri(@"http://littleplace.azurewebsites.net/"));
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
           var r= _cookies.GetCookies(new Uri(@"http://littleplace.azurewebsites.net/"));

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

        public async Task<Response<string>> UploadEventImage(byte[] image)
        {
            var uploadAvatarCommand = new UploadEventImageCommand(_httpClient);
            uploadAvatarCommand.Image = image;
            var result = await _executerService.ExecuteCommand(uploadAvatarCommand, false);
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

        //EVENTS

        public async Task<Response<List<Event>>> GetMyOwnEventsCommand()
        {
            var command = new GetMyOwnEventsCommand(_httpClient);
            return await _executerService.ExecuteCommand(command, true);
        }

        public async Task<Response<List<Event>>> GetMyInvitedEventsCommand()
        {
            var command = new GetMyInvitedEventsCommand(_httpClient);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddFriendToEvent(Event ev, User friend)
        {
            var command = new AddFriendToEventCommand(_httpClient,ev.EventId,friend.UserId);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddFriendsToEvent(Event ev, IEnumerable<User> friends)
        {
            var command = new AddFriendsToEventCommand(_httpClient,ev.EventId,friends.Select(x=>x.UserId).ToList());
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<Event>> AddEvent(Event ev)
        {
            var command = new AddEventCommand(_httpClient, ev.Name, ev.EventTime, ev.Latitude, ev.Longitude, ev.OwnerId, ev.Address, ev.Description,ev.ImageUrl);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddMessageToEvent(string message,Event ev)
        {
            var command = new AddMessageToEventCommand(_httpClient, message,ev.EventId);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<Message>>> GetMessagesFromEventCommand(Event ev)
        {
            var command = new GetMessagesFromEventCommand(_httpClient, ev.EventId);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<EventMember>>> GetFriendsFromEvent(Event ev)
        {
            var command = new GetFriendsFromEventCommand(_httpClient, ev);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> SentMessageToFriend(string message, int friendId)
        {
            var command = new SentPrivateMessageCommand(_httpClient,friendId,message);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<PrivateMessage>>> GetMyPrivateMessages()
        {
            var command = new GetMyPrivateMessagesCommand(_httpClient);
            return await _executerService.ExecuteCommand(command, false);
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
