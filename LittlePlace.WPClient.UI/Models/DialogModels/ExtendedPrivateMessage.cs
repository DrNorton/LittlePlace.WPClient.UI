using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Telerik.Windows.Controls;

namespace LittlePlace.WPClient.UI.Models.DialogModels
{
    public class ExtendedPrivateMessage:ConversationViewMessage
    {
        private User _toId;
        private User _fromId;
       
        public User FromId
        {
            get { return _fromId; }
            set { _fromId = value; }
        }

        public User ToId
        {
            get { return _toId; }
            set { _toId = value; }
        }

        public string FormattedTimeStamp
        {
            get
            {
                return this.TimeStamp.ToShortTimeString();
            }
        }

      
        public ExtendedPrivateMessage(string text, DateTime timeStamp, ConversationViewMessageType type,User to,User from) :
            base(text, timeStamp, type)
        {
            _toId = to;
            _fromId = from;
           
        }
    }
}
