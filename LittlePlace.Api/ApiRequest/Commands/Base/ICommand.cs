
using System.Threading.Tasks;

namespace LittlePlace.Api.ApiRequest.Commands.Base
{
    public interface ICommand<T>
    {
        string Url { get; }
        string ActionName { get; }
        bool IsCached { get; set; }
        Task<T> Execute();
        int BuildCacheKey();
    }
}
