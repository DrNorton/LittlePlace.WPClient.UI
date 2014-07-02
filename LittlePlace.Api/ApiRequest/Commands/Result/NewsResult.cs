using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LittlePlace.Api.ApiRequest.Commands.Result
{
    public class NewsResult
    {
        private int _id;
        private string _text;
        private string _image;
        private DateTime _createdTime;
        private string _title;

        [JsonProperty("Id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [JsonProperty("Text")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [JsonProperty("Title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        [JsonProperty("Image")]
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        [JsonProperty("CreatedTime")]
        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
    }
}
