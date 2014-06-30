using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class User :  INotifyPropertyChanged 
    {
        private int _userId;
        private string _login;
        private string _photoUrl;
        private string _firstName;
        private string _lastName;
        private string _telephoneNumber;
        private string _email;
        private string _textStatus;
        private string _rawPhotoString;

        [JsonProperty("UserId")]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [JsonProperty("Login")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        [JsonProperty("Photo")]
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set { _photoUrl = value; }
        }

        [JsonProperty("FirstName")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [JsonProperty("LastName")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [JsonProperty("TelephoneNumber")]
        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        [JsonProperty("Email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [JsonProperty("TextStatus")]
        public string TextStatus
        {
            get { return _textStatus; }
            set { _textStatus = value; }
        }

        [JsonIgnore]
        public string NameWithSurname
        {
            
            get { return String.Format("{0},{1}", FirstName, LastName); }
        }


        [JsonIgnore]
        public byte[] PhotoRaw { get; set; }

        [JsonProperty("RawPhotoString")]
        public string RawPhotoString
        {
            get { return _rawPhotoString; }
            set
            {
                _rawPhotoString = value;
                OnPropertyChanged("RawPhotoString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
