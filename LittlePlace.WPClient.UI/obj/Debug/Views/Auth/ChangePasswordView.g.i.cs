﻿#pragma checksum "C:\Users\WinPhone\Documents\Visual Studio 2012\Projects\свои\LittlePlace.WPClient.UI\LittlePlace.WPClient.UI\Views\Auth\ChangePasswordView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16461729D4565EE48F1742A93CFCF896"
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


namespace LittlePlace.WPClient.UI.Views.Auth {
    
    
    public partial class ChangePasswordView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar AppBar;
        
        internal Caliburn.Micro.BindableAppBar.BindableAppBarButton SaveNewPassword;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LittlePlace.WPClient.UI;component/Views/Auth/ChangePasswordView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.AppBar = ((LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar)(this.FindName("AppBar")));
            this.SaveNewPassword = ((Caliburn.Micro.BindableAppBar.BindableAppBarButton)(this.FindName("SaveNewPassword")));
        }
    }
}

