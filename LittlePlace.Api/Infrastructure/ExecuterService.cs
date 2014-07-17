using System;
using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Exceptions;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.Infrastructure
{
    public class ExecuterService : IExecuterService
    {
        private ICacheService _cacheService;
        public event Action<int,string> OnError;
        public event Action<Exception> OnInternalException;

        public ExecuterService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<T> ExecuteCommand<T>(ICommand<T> command, bool useCache) where T:IResponse,new()
        {
            try
            {
                T result;
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
                    result = await command.Execute();
                    if (result.ErrorCode == 0)
                    {
                        _cacheService.SaveCacheResult(command, result);
                    }
                    GetError(result);
                    return result;
                }
                result = await command.Execute();
                GetError(result);
                return result;
            }
            catch (ApiException e)
            {
                if (OnError != null)
                {
                    OnError(e.Code, e.Message);
                }
            }
            catch (Exception e)
            {
                OnInternalException(e);
            }
         
            
            var task = new Task<T>(() => new T());
            return await task;
        }

        private void GetError<T>(T result) where T : IResponse
        {
            if (result.ErrorCode != 0)
            {
                throw new ApiException(result.ErrorMessage,result.ErrorCode);
            }
        }
    }
}
