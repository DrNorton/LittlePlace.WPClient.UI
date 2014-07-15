using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.ApiRequest.Commands.Events
{
    public class AddEventCommand : BaseCommand<Response<Event>>
    {
        public override string ActionName
        {
            get { return "addevent"; }
        }

        public AddEventCommand(HttpClient httpClient,string name, DateTime eventTime,double latitude,double longitude,int ownerId,string address, string description,string imageUrl)
            : base("events", httpClient)
        {

            FullUrl = String.Format("{0}&name={1}&eventTime={2}&latitude={3}&longitude={4}&ownerId={5}&address={6}&description={7}&imageurl={8}", Url, name, eventTime, latitude, longitude, ownerId, address, description,imageUrl);
        }

       
    }
}
