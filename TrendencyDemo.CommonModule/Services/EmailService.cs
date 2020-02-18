using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Linq;
using TrendencyDemo.Common.Configs;
using TrendencyDemo.CommonModule.Aggregates;
using TrendencyDemo.CommonModule.Interfaces;
using TrendencyDemo.Dal.Enums;

namespace TrendencyDemo.CommonModule.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private readonly EmailConfigs _emailConfigs;
        private readonly EmailCredentials _emailCredentials;

        public EmailService(ServiceBaseAggregate serviceBaseAggregate,
            IOptions<EmailConfigs> emailConfigs,
            IOptions<EmailCredentials> emailCredentials)
            : base(serviceBaseAggregate)
        {
            _emailConfigs = emailConfigs.Value;
            _emailCredentials = emailCredentials.Value;
        }

        public void SendEmails()
        {
            var emails = _context.Emails
                .Where(e => e.EmailState == EmailState.Pending)
                .ToList();
            if (!emails.Any())
                return;

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_emailConfigs.Host, _emailConfigs.Port, _emailConfigs.EnableSsl);
                client.Authenticate(_emailCredentials.UserName, _emailCredentials.Password);

                foreach (var email in emails)
                {
                    email.TryCount += 1;

                    string FromAddress = _emailCredentials.EmailAddress;
                    string FromAdressTitle = "";

                    string ToAddress = email.To;
                    string ToAdressTitle = "";

                    string Subject = email.Subject;
                    string BodyContent = email.Body;

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                    mimeMessage.Subject = Subject;
                    mimeMessage.Body = new TextPart("plain")
                    {
                        Text = BodyContent
                    };

                    try
                    {
                        client.Send(mimeMessage);
                        email.EmailState = EmailState.Sent;
                    }
                    catch (Exception ex)
                    {
                        email.LastError = ex.ToString();
                        if (email.TryCount >= 5)
                        {
                            email.EmailState = EmailState.Failed;
                        }
                    }
                }
                client.Disconnect(true);
            }
            _context.SaveChanges();
        }
    }
}
