using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace LittlePlace.WPClient.UI.Maps.Models
{
    public class MapPushpinModel
    {
        private GeoCoordinate _position;
        private BitmapSource _imagePath;
        private DateTime _time;

        public GeoCoordinate Position
        {
            get { return _position; }
            set
            {
                _position = value;
        //        NotifyOfPropertyChange(()=>Position);
            }
        }

        public BitmapSource ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
               // NotifyOfPropertyChange(() => ImagePath);
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
             //   NotifyOfPropertyChange(() => Time);
            }
        }

        public MapPushpinModel()
        {
            _position=new GeoCoordinate();
        }
    }
}
