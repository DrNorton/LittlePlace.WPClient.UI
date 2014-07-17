using System.Collections.Generic;
using System.Net.Http;

namespace LittlePlace.Api.Infrastructure
{
    public interface ICommandProvider
    {
        T GetCommand<T>(HttpClient httpClient,Dictionary<string, string> parameters);
    }
}