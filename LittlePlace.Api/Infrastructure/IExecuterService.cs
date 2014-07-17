using System.Threading.Tasks;
using LittlePlace.Api.ApiRequest.Commands.Base;
using LittlePlace.Api.Models;

namespace LittlePlace.Api.Infrastructure
{
    public interface IExecuterService
    {
        Task<T> ExecuteCommand<T>(ICommand<T> command, bool useCache) where T : IResponse, new();
    }
}