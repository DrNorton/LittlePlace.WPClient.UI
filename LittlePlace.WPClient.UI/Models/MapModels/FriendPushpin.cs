using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Yandex.Maps;
using Yandex.Positioning;

namespace LittlePlace.WPClient.UI.Models.MapModels
{
    public class FriendPushpin:BasePushpin
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                base.NotifyOfPropertyChange(() => User);
            }
        }

        public FriendPushpin(User user, FriendPositionResult position)
        {
            _user = user;
          base.State=PushPinState.Expanded;;
          base.Visibility=Visibility.Visible;
          base.ZIndex = 0;
          base.ContentVisibility = Visibility.Visible;
          base.Position=new GeoCoordinate(position.Latitude,position.Longitude);
        }

        
    }
}
