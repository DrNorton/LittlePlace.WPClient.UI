using LittlePlace.Api.ApiRequest.Commands.Base;

namespace LittlePlace.Api.Infrastructure
{
    public interface ICacheService
    {
        T GetCachedResult<T>(ICommand<T> command);
        bool SaveCacheResult<T>(ICommand<T> command,T result);
    }
}