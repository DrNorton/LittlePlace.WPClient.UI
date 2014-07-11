using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Caliburn.Micro;
using LittlePlace.WPClient.UI.EventMessages;
using LittlePlace.WPClient.UI.EventMessages.Maps;
using LittlePlace.WPClient.UI.Models.MapModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Yandex.Maps;
using Yandex.Maps.ContentLayers;

namespace LittlePlace.WPClient.UI.Views
{
    public partial class MapView : PhoneApplicationPage,IHandle<NavigateMapToPushpinMessage>
    {
        private IEventAggregator _eventAggregator;
        public MapView()
        {
            InitializeComponent();
            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
            MapGlobalSettings.Instance.EnableLocationService = true;
           
        }




        public void Handle(NavigateMapToPushpinMessage message)
        {
            var pushPins = FindVisualChildren<PushPin>(friendLayer);
            foreach (var pushPin in pushPins)
            {
                var pushPinContext=(BasePushpin)pushPin.DataContext;
                if (pushPinContext.Position.Latitude.Equals(message.Position.Latitude) &&
                    pushPinContext.Position.Longitude.Equals(message.Position.Longitude))
                {
                    Map.CenterOnControl(pushPin);
                    pushPinContext.ContentVisibility = Visibility.Visible;
                }
            
          
            }

        }

        public  IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Item1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item3_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}