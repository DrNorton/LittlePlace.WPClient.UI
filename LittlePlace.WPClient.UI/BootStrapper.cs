using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Cache;
using LittlePlace.WPClient.UI.Services;
using LittlePlace.WPClient.UI.Services.Base;
using LittlePlace.WPClient.UI.ViewModels;
using LittlePlace.WPClient.UI.ViewModels.Auth;
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
            var cacheService = new CacheService(_dbDataContext);
            _container.RegisterInstance(typeof (ICacheService), null, cacheService);
            var executerService = new ExecuterService(cacheService);
            AddErrorSubscriptionToExecuterService(executerService);
            _container.RegisterSingleton(typeof (ILittlePlaceApiService), null, typeof (LittlePlaceApiService));
            _container.RegisterInstance(typeof (IExecuterService), null, executerService);
            _container.RegisterPerRequest(typeof (ISettingService), null, typeof (SettingService));
            _container.RegisterPerRequest(typeof(IGeoCodingService),null,typeof(GeoCodingService));
            _container.RegisterPerRequest(typeof(ICommandProvider),null,typeof(CommandProvider));
        }

        private void AddErrorSubscriptionToExecuterService(ExecuterService executerService)
        {
            executerService.OnError += executerService_OnError;
            executerService.OnInternalException += executerService_OnInternalException;
        }

        void executerService_OnInternalException(Exception obj)
        {
            MessageBox.Show(obj.Message);
        }

        void executerService_OnError(int code, string message)
        {
            switch (code)
            {
                case 401:
                    MessageBox.Show("Авторизуйтесь");
                    IoC.Get<INavigationService>().UriFor<AuthViewModel>().Navigate();
                    break;
            }
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
            _container.PerRequest<AddEventViewModel>();
            _container.PerRequest<EventsListViewModel>();
            _container.PerRequest<EventViewModel>();
            _container.PerRequest<DialogsListViewModel>();
            _container.PerRequest<DialogViewModel>();

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
