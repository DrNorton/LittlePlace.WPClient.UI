﻿#pragma checksum "C:\Users\WinPhone\Documents\Visual Studio 2012\Projects\свои\LittlePlace.WPClient.UI\LittlePlace.WPClient.UI\Views\MapView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "83F5F9F16CEDE74D915CA70F46701443"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro.BindableAppBar;
using LittlePlace.WPClient.UI.Extensions;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Yandex.Maps;
using Yandex.Maps.Behavior;
using Yandex.Maps.ContentLayers;


namespace LittlePlace.WPClient.UI.Views {
    
    
    public partial class MapView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid FriendPanel;
        
        internal System.Windows.Controls.Grid MapPanel;
        
        internal Yandex.Maps.Map Map;
        
        internal Yandex.Maps.Behavior.PushPinStateBehavior PushPinStateBehavior;
        
        internal Yandex.Maps.ContentLayers.LayerContainer LayerContainer;
        
        internal Yandex.Maps.MapLayer mapLayer2;
        
        internal Yandex.Maps.MapItemsControl friendLayer;
        
        internal LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar AppBar;
        
        internal Caliburn.Micro.BindableAppBar.BindableAppBarButton FriendShowPanel;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LittlePlace.WPClient.UI;component/Views/MapView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.FriendPanel = ((System.Windows.Controls.Grid)(this.FindName("FriendPanel")));
            this.MapPanel = ((System.Windows.Controls.Grid)(this.FindName("MapPanel")));
            this.Map = ((Yandex.Maps.Map)(this.FindName("Map")));
            this.PushPinStateBehavior = ((Yandex.Maps.Behavior.PushPinStateBehavior)(this.FindName("PushPinStateBehavior")));
            this.LayerContainer = ((Yandex.Maps.ContentLayers.LayerContainer)(this.FindName("LayerContainer")));
            this.mapLayer2 = ((Yandex.Maps.MapLayer)(this.FindName("mapLayer2")));
            this.friendLayer = ((Yandex.Maps.MapItemsControl)(this.FindName("friendLayer")));
            this.AppBar = ((LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar)(this.FindName("AppBar")));
            this.FriendShowPanel = ((Caliburn.Micro.BindableAppBar.BindableAppBarButton)(this.FindName("FriendShowPanel")));
        }
    }
}

