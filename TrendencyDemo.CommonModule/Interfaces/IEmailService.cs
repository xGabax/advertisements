
namespace TrendencyDemo.CommonModule.Interfaces
{
    public interface IEmailService
    {
        void AddEmail(string address, string subject, string body);
        void SendEmails();
    }
}
