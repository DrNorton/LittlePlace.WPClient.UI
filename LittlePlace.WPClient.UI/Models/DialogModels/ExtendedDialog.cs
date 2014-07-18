using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Telerik.Windows.Controls;

namespace LittlePlace.WPClient.UI.Models.DialogModels
{
    public class ExtendedDialog:PropertyChangedBase
    {
        private ObservableCollection<ExtendedPrivateMessage> _privateMessages;
        public string Id { get; set; }

        public User Me { get; set; }
        public User Member { get; set; }
        public ExtendedPrivateMessage LastMessage { get; set; }

        public ObservableCollection<ExtendedPrivateMessage> PrivateMessages
        {
            get { return _privateMessages; }
            set
            {
                _privateMessages = value;
                base.NotifyOfPropertyChange(()=>PrivateMessages);
            }
        }

        public ExtendedDialog()
        {
            _privateMessages=new ObservableCollection<ExtendedPrivateMessage>();
        }


        public static ExtendedDialog CreateExtendedDialog(IEnumerable<User> friends,User me,Dialog dialog)
        {
            var extendedDialog = new ExtendedDialog();
            extendedDialog.Me = me;
            extendedDialog.Id = dialog.Id;
            var notMeId = dialog.Members.FirstOrDefault(x => x.MemberId != me.UserId).MemberId;
            extendedDialog.Member = friends.FirstOrDefault(x => x.UserId == notMeId);
            extendedDialog.PrivateMessages = new ObservableCollection<ExtendedPrivateMessage>();
            foreach (var message in dialog.Messages)
            {
                var fromId = friends.FirstOrDefault(x => x.UserId == message.FromId);
                var toId =  friends.FirstOrDefault(x => x.UserId == message.ToId);
                ConversationViewMessageType type;
                if (message.FromId == notMeId)
                {
                    type = ConversationViewMessageType.Incoming;
                }
                else
                {
                    type = ConversationViewMessageType.Outgoing;
                }
                extendedDialog.PrivateMessages.Add(new ExtendedPrivateMessage(message.MessageText,message.SentTime,type, toId,fromId));
            }
            extendedDialog.LastMessage = extendedDialog.PrivateMessages.LastOrDefault();
            return extendedDialog;
        }



    }
}
