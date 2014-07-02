using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LittlePlace.WPClient.UI.Views
{
    public partial class AuthView : PhoneApplicationPage
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PasswordBox)sender).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
        }
    }
}