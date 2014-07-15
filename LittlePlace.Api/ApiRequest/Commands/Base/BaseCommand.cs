using System;
using System.Net.Http;
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
       

        public BaseCommand(string methodGroup, HttpClient restClient)
        {
            _methodGroup = methodGroup;
            _restClient = restClient;
            FullUrl = Url;
        }

        public abstract string ActionName { get; }
      

        public int BuildCacheKey()
        {
            return FullUrl.GetHashCode();
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
