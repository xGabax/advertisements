using System;
using TrendencyDemo.Dal.Enums;

namespace TrendencyDemo.Dal.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastTriedDate { get; set; }
        public int TryCount { get; set; }
        public string LastError { get; set; }

        public EmailState EmailState { get; set; }
    }

}
