using System.Net;

namespace LittlePlace.Api.Infrastructure
{
    public interface ISettingService
    {
        CookieContainer AuthCookies { get; set; }
    }
}