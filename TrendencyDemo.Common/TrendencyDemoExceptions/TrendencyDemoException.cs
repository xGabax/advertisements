using System;
using System.Net;

namespace TrendencyDemo.Common.TrendencyDemoExceptions
{
    public class TrendencyDemoException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public TrendencyDemoStatusCode TrendencyDemoStatusCode { get; set; }

        public TrendencyDemoException() 
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public TrendencyDemoException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public TrendencyDemoException(string message, Exception innerException) : base(message, innerException)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public TrendencyDemoException(HttpStatusCode httpStatusCode, TrendencyDemoStatusCode trendencyDemoStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            TrendencyDemoStatusCode = trendencyDemoStatusCode;
        }
        public TrendencyDemoException(HttpStatusCode httpStatusCode, TrendencyDemoStatusCode trendencyDemoStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            TrendencyDemoStatusCode = trendencyDemoStatusCode;
        }
        public TrendencyDemoException(HttpStatusCode httpStatusCode, TrendencyDemoStatusCode trendencyDemoStatusCode, string message, Exception innerException) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
            TrendencyDemoStatusCode = trendencyDemoStatusCode;
        }
    }
}
