﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Position
{
    public class GetAllFriendsPositionCommand : BaseCommand<Response<List<FriendPositionResult>>>
    {
        public override string ActionName
        {
            get { return "getallfriendsposition"; }
        }

        public GetAllFriendsPositionCommand(HttpClient httpClient)
            : base("position", httpClient,new Dictionary<string, string>())
        {
            this.IsCached = true;
        }

      
    }
}
