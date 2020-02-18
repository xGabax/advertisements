using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.Web.Infrastructure.Jobs
{
    public class EmailSenderJob
    {
        private readonly IEmailService _emailService;

        public EmailSenderJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void SendEmails()
        {
            _emailService.SendEmails();
        }
    }
}
