using System.Net.Mail;
using System.Net;
using WarhammerGameManager.Logic.Logical.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = _config["EmailSettings:SMTPHost"] ?? "",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["EmailSettings:SenderAddress"], _config["EmailSettings:SenderPassword"])
            };

            var message = new MailMessage(_config["EmailSettings:SenderAddress"] ?? "", email, subject, htmlMessage);

            message.IsBodyHtml = true;

            return client.SendMailAsync(message);
        }
    }
}
