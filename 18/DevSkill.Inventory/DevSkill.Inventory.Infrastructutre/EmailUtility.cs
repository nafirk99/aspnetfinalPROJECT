using DevSkill.Inventory.Domain;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructutre
{
    public class EmailUtility : IEmailUtility
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailUtility(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(string receiverEmail, string receiverName, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Host, _smtpSettings.Port,
                    _smtpSettings.SmtpEncryption != SmtpEncryptionTypes.Normal);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_smtpSettings.UserName, _smtpSettings.Password);
                client.Timeout = 600;

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
