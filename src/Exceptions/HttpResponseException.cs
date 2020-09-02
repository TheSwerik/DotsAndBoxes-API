using System;

namespace API.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseCode Status { get; set; } = HttpResponseCode.InternalServerError;
        public object Value { get; set; }
        public bool PrintStackTrace { get; set; } = false;
    }
}