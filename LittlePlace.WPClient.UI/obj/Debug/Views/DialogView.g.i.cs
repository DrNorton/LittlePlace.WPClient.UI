﻿#pragma checksum "C:\Users\WinPhone\Documents\Visual Studio 2012\Projects\свои\LittlePlace.WPClient.UI\LittlePlace.WPClient.UI\Views\DialogView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D467E75E56331291A6CBBEB59565BAB2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Telerik.Windows.Controls;


namespace LittlePlace.WPClient.UI.Views {
    
    
    public partial class DialogView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Telerik.Windows.Controls.RadConversationView conversationView;
        
        internal System.Windows.Controls.TextBlock typingTextBlock;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LittlePlace.WPClient.UI;component/Views/DialogView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.conversationView = ((Telerik.Windows.Controls.RadConversationView)(this.FindName("conversationView")));
            this.typingTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("typingTextBlock")));
        }
    }
}

