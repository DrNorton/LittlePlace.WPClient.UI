using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace LittlePlace.WPClient.UI.Models
{
    public class ContactForTileModel:PropertyChangedBase
    {
        private string _contactName;
        private string _picture;
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                base.NotifyOfPropertyChange(()=>Text);
            }
        }

        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                base.NotifyOfPropertyChange(()=>Picture);
            }
        }

        public string ContactName

        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                base.NotifyOfPropertyChange(()=>ContactName);
            }
        }
    }
}
