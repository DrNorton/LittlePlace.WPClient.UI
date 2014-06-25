using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LittlePlace.Api.ApiRequest.Commands.Base
{
    public abstract class BaseCommand<T> : ICommand<T>
    {
        private string _methodGroup;
        protected readonly HttpClient _restClient;
        private bool _isCached=false;

        public string Url
        {
            get { return String.Format("{0}/{1}", Constants.Host,_methodGroup); }
        }

        public string FullUrl { get; set; }

        public BaseCommand(string methodGroup, HttpClient restClient)
        {
            _methodGroup = methodGroup;
            _restClient = restClient;
        }

        public abstract string ActionName { get; }
        public abstract Task<T> Execute();

        public  string BuildCacheKey()
        {
            return FullUrl.GetHashCode().ToString();
        }

        public bool IsCached
        {
            get { return _isCached; }
            set { _isCached = value; }
        }
    }
}
