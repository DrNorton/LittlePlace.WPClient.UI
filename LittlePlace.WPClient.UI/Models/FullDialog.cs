using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;

namespace LittlePlace.WPClient.UI.Models
{
    public class FullDialog
    {
        private readonly Dialog _dialog;
        private List<FullPrivateMessage> _messages;

        public List<User> DialogMembers { get; set; }

        public List<FullPrivateMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        public FullDialog(Dialog dialog,IEnumerable<User> allFriends)
        {
            _dialog = dialog;
            DialogMembers=new List<User>();
            CreateFullDialog(allFriends);
        }

        private void CreateFullDialog(IEnumerable<User> allFriends)
        {
            foreach (var dialog in _dialog.Members)
            {
                DialogMembers.Add(allFriends.FirstOrDefault(x=>x.UserId==dialog.MemberId));
            }
            foreach (var message in _dialog.Messages)
            {
                Messages.Add(new FullPrivateMessage(message,DialogMembers));
            }
        }
    }
}
