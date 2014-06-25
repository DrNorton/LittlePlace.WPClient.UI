using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Cache;
using LittlePlace.WPClient.UI.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;

namespace LittlePlace.WPClient.UI
{
    public class BootStrapper : PhoneBootstrapper
    {
        private PhoneContainer _container;
        private LittlePlaceApiService _service;
        private CacheDataContext _dbDataContext;

        public BootStrapper()
        {
         
        }

        
        public PhoneContainer Container
        {
            get { return _container; }
        }
   

        protected override void Configure()
        {
            _container = new PhoneContainer();
            _container.RegisterPhoneServices(RootFrame);
            _container.PerRequest<MainViewModel>();
            _container.PerRequest<FriendsListViewModel>();
            _container.PerRequest<MapViewModel>();
            _dbDataContext = new CacheDataContext(@"Data Source=isostore:/ToDo.sdf");
            
                if (_dbDataContext.DatabaseExists() == false)
                {
                    // Create the database.
                    _dbDataContext.CreateDatabase();
                }
            
            _container.RegisterInstance(typeof(ICacheService),null,new CacheService(_dbDataContext));
            _container.RegisterSingleton(typeof(ILittlePlaceApiService),null,typeof(LittlePlaceApiService));
            _container.RegisterPerRequest(typeof(IExecuterService),null,typeof(ExecuterService));
            var bingMapProvider = new ApplicationIdCredentialsProvider("Au5Po_aD2JA2igWtvTycQIuz4tyc_7dKgTu8pDr85rlIvxOVzKOR2LrmGj3DlEWt");
            _container.RegisterInstance(typeof(ApplicationIdCredentialsProvider),null,bingMapProvider);

        }

        public void GoTo(string uri)
        {
            RootFrame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }


        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
           // FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }

        protected override void OnActivate(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            //FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new TransitionFrame();
        }

        protected override void OnClose(object sender, Microsoft.Phone.Shell.ClosingEventArgs e)
        {
            //FlurryWP8SDK.Api.EndSession();
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Произошло необработанное исключение; перейти в отладчик
                System.Diagnostics.Debugger.Break();
            }

            MessageBox.Show(e.ExceptionObject.Message, "Неожиданная ошибка", MessageBoxButton.OK);
        }
    }
    
}
