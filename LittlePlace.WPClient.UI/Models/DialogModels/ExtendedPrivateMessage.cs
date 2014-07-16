using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittlePlace.Api.ApiRequest.Commands.Result;

namespace LittlePlace.WPClient.UI.Models.DialogModels
{
    public class ExtendedPrivateMessage
    {
        public string MessageText { get; set; }
        public User FromId { get; set; }
        public User ToId { get; set; }
        public DateTime SentTime { get; set; }
    }
}
