using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Models.DialogModels;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels.Dialogs
{
    public class DialogsListViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<ExtendedDialog> _dialogs;
        private ExtendedDialog _selectedDialog;
        

        public DialogsListViewModel(ILittlePlaceApiService littlePlaceApiService,INavigationService navigationService)
        {
            _littlePlaceApiService = littlePlaceApiService;
            _navigationService = navigationService;
            _dialogs=new ObservableCollection<ExtendedDialog>();
            base.StartDataLoading();
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var dialogs = (await _littlePlaceApiService.GetMyDialogs()).Result;
            CreateFullDialog(dialogs);
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
          
           
        }

        private async void CreateFullDialog(List<Dialog> dialogs)
        {
            var friends=(await _littlePlaceApiService.GetMyFriends()).Result;
            var me = (await _littlePlaceApiService.GetMe()).Result;
            foreach (var dialog in dialogs)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>  Dialogs.Add(ExtendedDialog.CreateExtendedDialog(friends, me, dialog)));
            }
        }

        public ObservableCollection<ExtendedDialog> Dialogs
        {
            get { return _dialogs; }
            set
            {
                _dialogs = value;
                base.NotifyOfPropertyChange(() => Dialogs);
            }
        }

        public ExtendedDialog SelectedDialog
        {
            get { return _selectedDialog; }
            set
            {
                _selectedDialog = value;
                NavigateToDialogView(value);
                base.NotifyOfPropertyChange(()=>SelectedDialog);
            }
        }

        private void NavigateToDialogView(ExtendedDialog value)
        {
            if (value != null)
            {
                _navigationService.UriFor<DialogViewModel>().WithParam(x=>x.DialogId,value.Id).WithParam(x=>x.ToId,value.Member.UserId).Navigate();
            }
        }
    }
}
