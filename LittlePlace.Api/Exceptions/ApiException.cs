using System;

namespace LittlePlace.Api.Exceptions
{
    public class ApiException:Exception
    {
        private int _code;

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public ApiException(string message,int code)
        : base(message)
        {
            code = _code;
        }
    }
}
