using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;

namespace LittlePlace.WPClient.UI.Models
{
    public class FullPrivateMessage
    {
        private readonly PrivateMessage _privateMessage;
        private User _from;
        private User _to;

        public User From
        {
            get { return _from; }
        }

        public User To
        {
            get { return _to; }
        }

        public string MessageText
        {
            get { return _privateMessage.MessageText; }
        }
       
        public DateTime SentTime
        {
            get { return _privateMessage.SentTime; }
           
        }

        public FullPrivateMessage(PrivateMessage privateMessage,IEnumerable<User> dialogMembers )
        {
            _privateMessage = privateMessage;
            _from = dialogMembers.FirstOrDefault(x => x.UserId == _privateMessage.FromId);
            _to = dialogMembers.FirstOrDefault(x => x.UserId == _privateMessage.ToId);
        }
    }
}
