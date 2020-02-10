using TrendencyDemo.Common.TrendencyDemoExceptions;

namespace DrendencyDemo.Web.Models.Shared
{
    public class TrendencyDemoExceptionDto
    {
        public TrendencyDemoStatusCode TrendencyDemoStatusCode { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
