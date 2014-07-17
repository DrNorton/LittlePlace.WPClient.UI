using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;

namespace LittlePlace.Api.Infrastructure
{
    public class CommandProvider : ICommandProvider
    {
       public T GetCommand<T>(HttpClient httpClient,Dictionary<string, string> parameters) 
       {
           var command=(T)Activator.CreateInstance(typeof(T),httpClient, parameters);
           return command;
       }
    }
}
