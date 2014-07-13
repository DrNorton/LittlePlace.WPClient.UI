using System;
using Yandex.Maps.Geocoding;
using Yandex.Maps.Geocoding.Dto;
using Yandex.WebUtils.Events;

namespace LittlePlace.WPClient.UI.Services.Base
{
    public interface IGeoCodingService
    {
        Action<GeocodeObject> Completed { get; set; }
        Action<Exception> Failed { get; set; }
        void GetAddress(double latitude,double longitude);
    }
}