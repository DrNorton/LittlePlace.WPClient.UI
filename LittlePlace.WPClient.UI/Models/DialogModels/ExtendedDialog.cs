using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;

namespace LittlePlace.WPClient.UI.Models.DialogModels
{
    public class ExtendedDialog
    {
        public string Id { get; set; }

        public User Me { get; set; }
        public User Member { get; set; }
        public ExtendedPrivateMessage LastMessage { get; set; }
        public List<ExtendedPrivateMessage> PrivateMessages { get; set; } 


        public static ExtendedDialog CreateExtendedDialog(IEnumerable<User> friends,User me,Dialog dialog)
        {
            var extendedDialog = new ExtendedDialog();
            extendedDialog.Me = me;
            extendedDialog.Id = dialog.Id;
            var notMeId = dialog.Members.FirstOrDefault(x => x.MemberId != me.UserId).MemberId;
            extendedDialog.Member = friends.FirstOrDefault(x => x.UserId == notMeId);
            extendedDialog.PrivateMessages=new List<ExtendedPrivateMessage>();
            foreach (var message in dialog.Messages)
            {
               extendedDialog.PrivateMessages.Add(new ExtendedPrivateMessage(){FromId = friends.FirstOrDefault(x=>x.UserId==message.FromId),
                   MessageText = message.MessageText,
                   SentTime = message.SentTime,
                   ToId = friends.FirstOrDefault(x=>x.UserId==message.ToId)}); 
            }
            extendedDialog.LastMessage = extendedDialog.PrivateMessages.LastOrDefault();
            return extendedDialog;
        }



    }
}
