using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.Api.Infrastructure;
using LittlePlace.WPClient.UI.ViewModels.Base;

namespace LittlePlace.WPClient.UI.ViewModels
{
    public class DialogViewModel:LoadingScreen
    {
        private readonly ILittlePlaceApiService _littlePlaceApiService;
        private Dialog _dialog;
        public string DialogId { get; set; }
        public int ToId { get; set; }
        private string _message;
        private IObservable<long> _observable;


        public Dialog Dialog
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
           _observable = Observable.Interval(TimeSpan.FromSeconds(2));
           
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
            _observable.Subscribe(async x => await GetDialog());
        }

        private async Task GetDialog()
        {
             Dialog = (await _littlePlaceApiService.GetMyDialogById(DialogId)).Result;
        }

        public async void SentMessage()
        {
            var result=await _littlePlaceApiService.SentPrivateMessage(ToId,Message);
            await GetDialog();
        }

        protected override void FirstDataLoadedCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
          //  throw new NotImplementedException();
        }
    }
}
