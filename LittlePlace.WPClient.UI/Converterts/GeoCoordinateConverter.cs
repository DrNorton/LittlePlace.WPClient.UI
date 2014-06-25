using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Microsoft.Phone.Controls.Maps.Platform;

namespace LittlePlace.WPClient.UI.Converterts
{
    public class GeoCoordinateConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = value as FriendPositionResult;
            var resLocation = new Location();
            resLocation.Latitude = result.Latitude;
            resLocation.Longitude = result.Longitude;
            return resLocation;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
