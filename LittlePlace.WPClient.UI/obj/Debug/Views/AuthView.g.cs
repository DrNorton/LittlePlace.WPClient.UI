﻿#pragma checksum "C:\Users\drnor_000\Documents\Visual Studio 2012\Projects\LittlePlace.WPClient.UI\LittlePlace.WPClient.UI\Views\AuthView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "45B7568859E68D5620A268549CACEDAA"
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
using LittlePlace.WPClient.UI.Controls;
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


namespace LittlePlace.WPClient.UI.Views {
    
    
    public partial class AuthView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal LittlePlace.WPClient.UI.Controls.TextBoxCaptioned LoginBox;
        
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        internal System.Windows.Controls.Button Logon;
        
        internal LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar AppBar;
        
        internal Caliburn.Micro.BindableAppBar.BindableAppBarButton Вход;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LittlePlace.WPClient.UI;component/Views/AuthView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.LoginBox = ((LittlePlace.WPClient.UI.Controls.TextBoxCaptioned)(this.FindName("LoginBox")));
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(this.FindName("PasswordBox")));
            this.Logon = ((System.Windows.Controls.Button)(this.FindName("Logon")));
            this.AppBar = ((LittlePlace.WPClient.UI.Extensions.EnabledBindableAppBar)(this.FindName("AppBar")));
            this.Вход = ((Caliburn.Micro.BindableAppBar.BindableAppBarButton)(this.FindName("Вход")));
        }
    }
}

