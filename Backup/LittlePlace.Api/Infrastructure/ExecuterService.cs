using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;

namespace LittlePlace.Api.Infrastructure
{
    public class ExecuterService
    {
        private ICacheService _cacheService;

        public ExecuterService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<T> ExecuteCommand<T>(ICommand<T> command,bool useCache)
        {
            if (useCache && command.IsCached)
            {
               var res= _cacheService.GetCachedResult(command);
                //Ищем результат по ключу

            }
            
            var result= await command.Execute();
            return default(T);
        }
    }
}
