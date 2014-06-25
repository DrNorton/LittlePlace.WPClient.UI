using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittlePlace.WPClient.UI.EventMessages
{
    public class NewPositionEventMessage
    {
        private double _latitude;
        private double _longitude;
        private DateTime _time;

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
