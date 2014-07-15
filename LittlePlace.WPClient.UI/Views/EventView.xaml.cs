using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro.BindableAppBar;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LittlePlace.WPClient.UI.Views
{
    public partial class EventView : PhoneApplicationPage
    {
        public EventView()
        {
            InitializeComponent();
         
        }

        private void TextInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((PhoneTextBox)sender).GetBindingExpression(PhoneTextBox.TextProperty).UpdateSource();
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var pivotItem= (PivotItem)e.AddedItems[0];
            if ((string) pivotItem.Header == "Описание")
            {
                SentMessage.Visibility = Visibility.Collapsed;
            }

            if ((string) pivotItem.Header == "Диалоги")
            {
                SentMessage.Visibility = Visibility.Visible;
            }

           
        }
    }
}