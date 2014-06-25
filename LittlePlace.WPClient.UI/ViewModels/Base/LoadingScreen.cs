using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;

namespace LittlePlace.WPClient.UI.ViewModels.Base
{
    public class LoadingScreen:Screen
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                base.NotifyOfPropertyChange(()=>IsLoading);
            }
        }

        public void ProgressIndicatorStatus(bool status)
        {
            IsLoading = status;
        }

        public LoadingScreen()
        {
            AddCustomConventions();
        }
        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<BindableAppBarButton>(
            Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
            Control.IsEnabledProperty, "DataContext", "Click");
        }
    }
}
