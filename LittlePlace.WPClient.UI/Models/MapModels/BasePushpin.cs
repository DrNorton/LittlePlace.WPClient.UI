using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using Yandex.Maps;
using Yandex.Positioning;

namespace LittlePlace.WPClient.UI.Models.MapModels
{
    public class BasePushpin : PropertyChangedBase
    {
        public Visibility Visibility { get; set; }
        public Visibility ContentVisibility { get; set; }
        public PushPinState State { get; set; }
        public GeoCoordinate Position { get; set; }
        public int ZIndex { get; set; }
    }
}
