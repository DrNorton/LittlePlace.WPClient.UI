using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LittlePlace.Api.Models;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Base
{
    public abstract class BaseCommand<T> : ICommand<T> 
    {
        private string _methodGroup;
        protected readonly HttpClient _restClient;
        private bool _isCached=false;

        public string Url
        {
            get { return String.Format("{0}/{1}/{2}?client={3}", Constants.Host, _methodGroup,ActionName,"winphone"); }
        }

        public string FullUrl { get; set; }

        private string _cacheUrl { get; set; }
       

        public BaseCommand(string methodGroup, HttpClient restClient,Dictionary<string,string> parameters)
        {
            _methodGroup = methodGroup;
            _restClient = restClient;
            ConvertDictToUrl(parameters,Url);
        }

        public BaseCommand()
        {
            
        }

        private void ConvertDictToUrl(Dictionary<string, string> parameters,string url)
        {
            var builder = new StringBuilder();
            builder.Append(Url);
            foreach (var parameter in parameters)
            {
                builder.Append(String.Format("&{0}={1}", parameter.Key,parameter.Value));
            }
            _cacheUrl = builder.ToString();
            builder.Append(String.Format("&{0}={1}", "requestid", Guid.NewGuid().ToString()));
            FullUrl = builder.ToString();
        }

        public abstract string ActionName { get; }
      

        public int BuildCacheKey()
        {
            return _cacheUrl.GetHashCode();
        }

        public bool IsCached
        {
            get { return _isCached; }
            set { _isCached = value; }
        }

        public async virtual System.Threading.Tasks.Task<T> Execute()
        {
            var responseString = await _restClient.GetStringAsync(FullUrl);
            var results =Deserialize(responseString);
            return results;
        }

        public  T Deserialize(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
