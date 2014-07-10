using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using LittlePlace.Api.ApiRequest.Commands.Result;
using LittlePlace.WPClient.UI.EventMessages;
using Yandex.Maps;
using Yandex.Positioning;

namespace LittlePlace.WPClient.UI.Models.MapModels
{
    public class BasePushpin : PropertyChangedBase
    {
        protected Visibility _visibility;
        protected Visibility _contentVisibility;
        protected PushPinState _state=PushPinState.Expanded;
        protected int _zIndex;
        protected GeoCoordinate _position;

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                base.NotifyOfPropertyChange(()=>Visibility);
            }
        }

        public Visibility ContentVisibility
        {
            get { return _contentVisibility; }
            set
            {
                _contentVisibility = value;
                if (value == Visibility.Visible)
                {
                    ZIndex++;
                }
                else
                {
                    ZIndex--;
                }
                base.NotifyOfPropertyChange(()=>ContentVisibility);
            }
        }

        public PushPinState State
        {
            get { return _state; }
            set
            {
                _state = value;
                base.NotifyOfPropertyChange(()=>State);
            }
        }

        public GeoCoordinate Position
        {
            get { return _position; }
            set
            {
                _position = value;
                base.NotifyOfPropertyChange(()=>Position);
            }
        }

        public int ZIndex
        {
            get { return _zIndex; }
            set
            {
                _zIndex = value;
                base.NotifyOfPropertyChange(()=>ZIndex);
            }
        }

        
    }
}
