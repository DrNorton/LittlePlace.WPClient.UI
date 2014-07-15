using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Users
{
    public class UpdateMeCommand: BaseCommand<Response<string>>
    {

        public override string ActionName
        {
            get { return "updateme"; }
        }

        public UpdateMeCommand(HttpClient httpClient,User updatedUser)
            : base("user", httpClient)
        {
            FullUrl = String.Format("{0}&login={1}&photo={2}&firstname={3}&lastname={4}&telephonenumber={5}&email={6}", Url,
                updatedUser.Login,updatedUser.PhotoUrl,updatedUser.FirstName,updatedUser.LastName,updatedUser.TelephoneNumber,updatedUser.Email);
        }

        

     
    }
}
