using System;
using System.Threading.Tasks;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.Models.DialogModels;
using LittlePlace.WPClient.UI.ViewModels.Base;
using Telerik.Windows.Controls;

namespace LittlePlace.WPClient.UI.ViewModels.Dialogs
{
    public class DialogViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private ExtendedDialog _dialog;
        public string DialogId { get; set; }
        public int ToId { get; set; }
        private string _message;
        private IObservable<long> _observable;


        public ExtendedDialog Dialog
        {
            get { return _dialog; }
            set
            {
                _dialog = value;
                base.NotifyOfPropertyChange(()=>Dialog);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                base.NotifyOfPropertyChange(()=>Message);
            }
        }

        public DialogViewModel(ILittlePlaceApiService littlePlaceApiService)
        {
            _littlePlaceApiService = littlePlaceApiService;
        //   _observable = Observable.Interval(TimeSpan.FromSeconds(2));
           
            // Subscribe the obserable to the task on execution.
        
        }

        async void  dt_Tick(object sender, EventArgs e)
        {
           await GetDialog();
        }

        protected override void OnViewReady(object view)
        {
            base.StartDataLoading();
            base.OnViewReady(view);
        }

        protected async override void DataLoading(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            await GetDialog();
          //  _observable.Subscribe(async x => await GetDialog());
        }

        private async Task GetDialog()
        {
             var dialog = (await _littlePlaceApiService.GetMyDialogById(DialogId)).Result;
            var friends = (await _littlePlaceApiService.GetMyFriends()).Result;
            var me=(await _littlePlaceApiService.GetMe()).Result;
           Dialog= ExtendedDialog.CreateExtendedDialog(friends, me, dialog);
        }

        public async void SentMessage(ConversationViewMessageEventArgs args)
        {
            var message = (ConversationViewMessage) args.Message;
            var result=await _littlePlaceApiService.SentPrivateMessage(ToId,message.Text);
            await GetDialog();
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
          //  throw new NotImplementedException();
        }
    }
}
