using System;
using System.Device.Location;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.WPClient.UI.EventMessages;
using Microsoft.Phone.Controls;

namespace LittlePlace.WPClient.UI
{
    public partial class App : Application
    {
        private IEventAggregator _eventAggregator;
        private GeoCoordinateWatcher _watcher;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            InitializeComponent();
            _eventAggregator=IoC.Get<IEventAggregator>();

            if (_watcher == null)
            {
                _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy
                _watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal
                _watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(_watcher_StatusChanged);
                _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(_watcher_PositionChanged);

            }
             _watcher.Start();
        }

        void _watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
           
            }
        }
        void _watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
           var latitude= e.Position.Location.Latitude;
           var longitude = e.Position.Location.Longitude;
            _eventAggregator.Publish(new NewPositionEventMessage(){Latitude = latitude,Longitude = longitude,Time = e.Position.Timestamp.DateTime});
           Debug.WriteLine("{0}|{1}",latitude,longitude);
        }


    }
}