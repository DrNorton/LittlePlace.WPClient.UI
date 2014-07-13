using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LittlePlace.WPClient.UI.Services.Base;
using Yandex.Maps.Geocoding;
using Yandex.Maps.Geocoding.Dto;
using Yandex.Maps.Geocoding.Interfaces;
using Yandex.Maps.Helpers;
using Yandex.Positioning;
using Yandex.WebUtils.Events;

namespace LittlePlace.WPClient.UI.Services
{
    public class GeoCodingService : IGeoCodingService
    {
        private readonly IGeocodeManager _geocodeManager;
        
        public GeoCodingService()
        {
            _geocodeManager = GeocodeManagerProvider.GetGeocodeManager();
            _geocodeManager.GeocodeCompleted += _geocodeManager_GeocodeCompleted;
            _geocodeManager.GeocodeFailed += _geocodeManager_GeocodeFailed;
        }

        void _geocodeManager_GeocodeFailed(object sender, RequestFailedEventArgs<GeocodeRequestParameters> e)
        {
            Failed(e.Exception);
        }

        void _geocodeManager_GeocodeCompleted(object sender, RequestCompletedEventArgs<GeocodeRequestParameters, GeocodeResult> e)
        {
           var geoCodingObject=e.RequestResults.Addresses.FirstOrDefault();
           Completed(geoCodingObject);
        }

        public Action<GeocodeObject> Completed { get; set; }

        public Action<Exception> Failed { get; set; }


        public void GetAddress(double latitude,double longitude)
        {
            var coordinate = new GeoCoordinate(latitude, longitude);
            _geocodeManager.Query(new GeocodeRequestParameters(coordinate, this));
        }
    }
}
