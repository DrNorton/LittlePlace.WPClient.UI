using System.Windows;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.WPClient.UI.Models.MapModels;
using Yandex.Maps;
using Yandex.Positioning;

namespace LittlePlace.WPClient.UI.EventMessages.Maps
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
          base._visibility=Visibility.Visible;
          base._zIndex = 0;
          base._contentVisibility = Visibility.Collapsed;
          base._position=new GeoCoordinate(position.Latitude,position.Longitude);
        }

        
    }
}
