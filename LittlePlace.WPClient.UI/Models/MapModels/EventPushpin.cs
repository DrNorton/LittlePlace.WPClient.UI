using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Yandex.Positioning;

namespace LittlePlace.WPClient.UI.Models.MapModels
{
    public class EventPushpin:BasePushpin
    {
        private Event _event;

        public Event Event
        {
            get { return _event; }
           
        }

        public EventPushpin(Event ev)
        {
            _event = ev;
            base._visibility = System.Windows.Visibility.Visible;
            base._zIndex = 0;
            base._contentVisibility = System.Windows.Visibility.Collapsed;
           base.Position=new Yandex.Positioning.GeoCoordinate(ev.Latitude,ev.Longitude);
        }
    }
}
