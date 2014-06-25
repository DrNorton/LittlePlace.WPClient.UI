using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;


namespace LittlePlace.Api.Models
{
    public interface IResponse
    {
        [JsonProperty("errorMessage")]
        string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        int ErrorCode { get; set; }

       
    }

    public class Response<T> : IResponse
    {
        private string _errorMessage;
        private int _errorCode;
        private T _result;

        [JsonProperty("errorMessage")]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        [JsonProperty("errorCode")]
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        [JsonProperty("result")]
        public T Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public static Response<T>  Deserialize(string text)
        {
            return JsonConvert.DeserializeObject<Response<T>>(text);
        }
    }
}
