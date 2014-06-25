using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Cache;
using LittlePlace.Api.Infrastructure;
using Newtonsoft.Json;

namespace LittlePlace.WPClient.UI.Cache
{
    public class CacheService : ICacheService
    {
        private readonly CacheDataContext _dataContext;

        public CacheService(CacheDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public T GetCachedResult<T>(ICommand<T> command)
        {
            var cacheKey = command.BuildCacheKey();
            var result=_dataContext.CasheResults.FirstOrDefault(x => x.Key==cacheKey);
            if (result == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(result.ResultText,0,result.ResultText.Length));
        }

        public bool SaveCacheResult<T>(ICommand<T> command,T result)
        {
            var cacheKey = command.BuildCacheKey();
            var jsonText = JsonConvert.SerializeObject(result);
            //Вставляем результат
            _dataContext.CasheResults.InsertOnSubmit(new CacheResult(){Key = cacheKey,ResultText =Encoding.UTF8.GetBytes(jsonText)});
            _dataContext.SubmitChanges();
            return true;
        } 
    }
}
