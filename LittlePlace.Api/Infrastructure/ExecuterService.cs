using System;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.Infrastructure
{
    public class ExecuterService : IExecuterService
    {
        private ICacheService _cacheService;

        public ExecuterService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<T> ExecuteCommand<T>(ICommand<T> command, bool useCache) where T:IResponse
    {
        if (useCache)
        {
            if (command.IsCached)
            {
                //Ищем результат по ключу
                var res = _cacheService.GetCachedResult(command);
                if (res != null)
                {
                    return res;
                }
            }
            var result = await command.Execute();
            if (result.ErrorCode == 0)
            {
                _cacheService.SaveCacheResult(command, result);
            }
        
            return result;
        }
        return await command.Execute();
    }
    }
}
