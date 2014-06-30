using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro.BindableAppBar;

namespace LittlePlace.WPClient.UI.Extensions
{
    public class EnabledBindableAppBar : BindableAppBar
    {
        public EnabledBindableAppBar()
        {
            this.IsEnabledChanged += EnabledBindableAppBar_IsEnabledChanged;
        }

        void EnabledBindableAppBar_IsEnabledChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var bar = sender as BindableAppBar;
            foreach (var button in bar.Buttons)
            {
                ((BindableAppBarButton)button).Button.IsEnabled = (bool)e.NewValue;
            }
        }
    }
}
