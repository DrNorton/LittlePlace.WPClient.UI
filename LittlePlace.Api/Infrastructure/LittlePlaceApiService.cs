using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Auth;
using LittlePlace.Api.ApiRequest.Commands.Base;
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
        Task<Response<List<Dialog>>> GetMyDialogs();
        Task<Response<Dialog>> GetMyDialogById(string id);
        Task<Response<string>> SentPrivateMessage(int toid, string message);
    }

    public class LittlePlaceApiService : ILittlePlaceApiService
    {
        private readonly IExecuterService _executerService;
        private readonly ISettingService _settingService;
        private readonly ICacheService _cacheService;
        private readonly ICommandProvider _commandProvider;
        private HttpClient _httpClient;
        private CookieContainer _cookies;
        private bool _isAuthorizated;

        public LittlePlaceApiService(IExecuterService executerService,ISettingService settingService,ICacheService cacheService,ICommandProvider commandProvider)
        {
            _executerService = executerService;
            _settingService = settingService;
            _cacheService = cacheService;
            _commandProvider = commandProvider;
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

        public async Task<Response<string>> Logon(string login,string pass)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("login",login);
            dict.Add("pass",pass);
            var logonCommand= _commandProvider.GetCommand<LogonCommand>(_httpClient,dict);
            var result= await _executerService.ExecuteCommand(logonCommand,false);
            _settingService.AuthCookies = _cookies;
            return result;
        }


        public async Task<Response<string>> Logoff()
        {
            var logoffCommand = _commandProvider.GetCommand<LogoffCommand>(_httpClient,new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(logoffCommand, false);
        }

        public async Task<Response<RegisterResult>> Register(string login,string pass)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("login", login);
            dict.Add("pass", pass);
            var registerCommand = _commandProvider.GetCommand<RegisterCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(registerCommand,false);
        }

        public async Task<Response<string>> ChangePasssword(string oldPass, string newPass)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("oldpass", oldPass);
            dict.Add("newpass", newPass);
            var registerCommand = _commandProvider.GetCommand<ChangePasswordCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(registerCommand, false);
        }

        public async Task<Response<List<User>>> GetMyFriends()
        {
             var getMyFriendsCommand=_commandProvider.GetCommand<GetMyFriendsCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<string>> UploadAvatar(byte[] image)
        {
           var uploadAvatarCommand = _commandProvider.GetCommand<UploadAvatarCommand>(_httpClient,new Dictionary<string, string>());
            uploadAvatarCommand.Image = image;
            var result= await _executerService.ExecuteCommand(uploadAvatarCommand, false);
            _cacheService.RemoveCacheResult(_commandProvider.GetCommand<GetMeCommand>(_httpClient,new Dictionary<string, string>()));
            return result;
        }

        public async Task<Response<string>> UploadEventImage(byte[] image)
        {
            var command = _commandProvider.GetCommand<UploadEventImageCommand>(_httpClient, new Dictionary<string, string>());
            command.Image = image;
            var result = await _executerService.ExecuteCommand(command, false);
            return result;
        }


        public async Task<Response<User>> GetMe()
        {
            var command = _commandProvider.GetCommand<GetMeCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(command, true);
        }

        public async Task<Response<User>> GetUserByUserId(int userId)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("friendId",userId.ToString());
            var getMyFriendsCommand = _commandProvider.GetCommand<GetByUserIdCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<string>> UpdateMe(User updatedUser)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("login",updatedUser.Login);
            dict.Add("photo",updatedUser.PhotoUrl);
            dict.Add("firstname",updatedUser.FirstName);
            dict.Add("lastname",updatedUser.LastName);
            dict.Add("telephonenumber",updatedUser.TelephoneNumber);
            dict.Add("email",updatedUser.Email);
            var getMyFriendsCommand=_commandProvider.GetCommand<UpdateMeCommand>(_httpClient, dict);
            var result= await _executerService.ExecuteCommand(getMyFriendsCommand, false);
            _cacheService.RemoveCacheResult(_commandProvider.GetCommand<GetMeCommand>(_httpClient,new Dictionary<string, string>()));
            return result;
        }

        public async Task<Response<List<FriendPositionResult>>> GetAllFriendsPosition()
        {
            var getMyFriendsCommand = _commandProvider.GetCommand<GetAllFriendsPositionCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(getMyFriendsCommand, true);
        }

        public async Task<Response<List<NewsResult>>> GetAllNews()
        {
            var getMyFriendsCommand = _commandProvider.GetCommand<GetAllNewsCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(getMyFriendsCommand, false);
        }

        public async Task<Response<NewsResult>> GetNewsById(int newsId)
        {
            var dict = new Dictionary<string, string>();
           dict.Add("newsId",newsId.ToString());
            var getnewsById = _commandProvider.GetCommand<GetNewsByIdCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(getnewsById, false);
        }

        public async Task<Response<string>> AddMyPosition(double latitude,double longitude)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("latitude",latitude.ToString());
            dict.Add("longitude",longitude.ToString());
            var addPosition = _commandProvider.GetCommand<AddMyPositionCommand>(_httpClient, dict);
           return await _executerService.ExecuteCommand(addPosition,false);
        }

        public async Task<Response<List<UserPosition>>> GetFriendPosition(int friendId)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("friendId",friendId.ToString());
            var getFriendPosition = _commandProvider.GetCommand<GetFriendPositionCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(getFriendPosition,false);
        }

        //EVENTS

        public async Task<Response<List<Event>>> GetMyOwnEventsCommand()
        {
            var command = _commandProvider.GetCommand<GetMyOwnEventsCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(command, true);
        }

        public async Task<Response<List<Event>>> GetMyInvitedEventsCommand()
        {

            var command = _commandProvider.GetCommand<GetMyInvitedEventsCommand>(_httpClient,
                new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddFriendToEvent(Event ev, User friend)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("friendId",friend.UserId.ToString());
            dict.Add("eventId",ev.EventId.ToString());
            var command = _commandProvider.GetCommand<AddFriendToEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddFriendsToEvent(Event ev, IEnumerable<User> friends)
        {
            var friendListId = friends.Select(x => x.UserId).ToList();
            var str = "[";
            for(int i=0;i<friendListId.Count;i++)
            {
                str += friendListId[i];
                if (i != friendListId.Count - 1)
                {
                    str += ",";
                }
            }
            str+= "]";
            var dict = new Dictionary<string, string>();
            dict.Add("friends",str);
            dict.Add("eventId",ev.EventId.ToString());

            var command = _commandProvider.GetCommand<AddFriendsToEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<Event>> AddEvent(Event ev)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("name",ev.Name);
            dict.Add("eventTime",ev.EventTime.ToString());
            dict.Add("latitude",ev.Latitude.ToString());
            dict.Add("longitude",ev.Longitude.ToString());
            dict.Add("ownerId",ev.OwnerId.ToString());
            dict.Add("address",ev.Address);
            dict.Add("description",ev.Description);
            dict.Add("imageurl",ev.ImageUrl);
            var command = _commandProvider.GetCommand<AddEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> AddMessageToEvent(string message,Event ev)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("message", message);
            dict.Add("eventid", ev.EventId.ToString());
            var command = _commandProvider.GetCommand<AddMessageToEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<Message>>> GetMessagesFromEventCommand(Event ev)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("eventid", ev.EventId.ToString());
            var command = _commandProvider.GetCommand<GetMessagesFromEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<EventMember>>> GetFriendsFromEvent(Event ev)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("eventId", ev.EventId.ToString());
            var command = _commandProvider.GetCommand<GetFriendsFromEventCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<List<Dialog>>> GetMyDialogs()
        {

            var command = _commandProvider.GetCommand<GetMyDialogsCommand>(_httpClient, new Dictionary<string, string>());
            return await _executerService.ExecuteCommand(command, true);
        }

        public async Task<Response<Dialog>> GetMyDialogById(string id)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("id",id);
            var command = _commandProvider.GetCommand<GetDialogByIdCommand>(_httpClient, dict);
            return await _executerService.ExecuteCommand(command, false);
        }

        public async Task<Response<string>> SentPrivateMessage(int toid, string message)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("toid", toid.ToString());
            dict.Add("message", message);
            var command = _commandProvider.GetCommand<SentPrivateMessageCommand>(_httpClient, dict);
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
