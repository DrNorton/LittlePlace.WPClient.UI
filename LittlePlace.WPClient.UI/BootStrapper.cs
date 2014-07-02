﻿using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Cache;
using LittlePlace.WPClient.UI.Services;
using LittlePlace.WPClient.UI.ViewModels;
using Microsoft.Phone.Controls;

using ISettingService = LittlePlace.Api.Infrastructure.ISettingService;

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
            RegisterViewModels();
            DataRegister();
            RegisterServices();

        }

        private void DataRegister()
        {
            _dbDataContext = new CacheDataContext(@"Data Source=isostore:/ToDo.sdf");

            if (_dbDataContext.DatabaseExists() == false)
            {
                // Create the database.
                _dbDataContext.CreateDatabase();
            }
        }

        private void RegisterServices()
        {
            _container.RegisterInstance(typeof (ICacheService), null, new CacheService(_dbDataContext));
            _container.RegisterSingleton(typeof (ILittlePlaceApiService), null, typeof (LittlePlaceApiService));
            _container.RegisterPerRequest(typeof (IExecuterService), null, typeof (ExecuterService));
            _container.RegisterPerRequest(typeof (ISettingService), null, typeof (SettingService));
          
        }

        private void RegisterViewModels()
        {
            _container.RegisterPhoneServices(RootFrame);
            _container.PerRequest<MainViewModel>();
            _container.PerRequest<MyContactDetailViewModel>();
            _container.PerRequest<MapViewModel>();
            _container.PerRequest<ContactsViewModel>();
            _container.PerRequest<FriendContactDetailViewModel>();
            _container.PerRequest<ChangePasswordViewModel>();
            _container.PerRequest<SingleNewsViewModel>();
            _container.PerRequest<AuthViewModel>();
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
