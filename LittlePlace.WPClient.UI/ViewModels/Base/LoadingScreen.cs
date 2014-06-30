using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;

namespace LittlePlace.WPClient.UI.ViewModels.Base
{
    public abstract class LoadingScreen:Screen
    {
        private bool _isLoading;
        private bool _isAllMenuEnabled = true;
        private BackgroundWorker _threadWhoLoadFirstData;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                base.NotifyOfPropertyChange(()=>IsLoading);
            }
        }

        public bool IsAllMenuEnabled
        {
            get { return _isAllMenuEnabled; }
            set
            {
                _isAllMenuEnabled = value;
                base.NotifyOfPropertyChange(()=>IsAllMenuEnabled);
            }
        }


        public void ProgressIndicatorStatus(bool status)
        {
            IsLoading = status;
            IsAllMenuEnabled = !status;
            _threadWhoLoadFirstData=new BackgroundWorker();
            _threadWhoLoadFirstData.RunWorkerCompleted += FirstDataLoadedCompleted;
            _threadWhoLoadFirstData.DoWork+=DataLoading;
           
        }

        protected void StartDataLoading()
        {
            _threadWhoLoadFirstData.RunWorkerAsync();
        }

        protected abstract void DataLoading(object sender, DoWorkEventArgs e);

        protected abstract void FirstDataLoadedCompleted(object sender, RunWorkerCompletedEventArgs e);
      

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
