using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using GymAppAPI.Models.Common;
using Microsoft.Extensions.Options;
using GymAppAPI.Models.Request;

namespace GymAppAPI.Services
{
    public class EmailService : IEmailService
    {          
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings, IOptions<AppSettings> appsetiongs) 
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(EmailRequest oModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", _smtpSettings.Username));

            // Use the correct constructor for MailboxAddress
            message.To.Add(new MailboxAddress("Javier", oModel.To));

            message.Subject = oModel.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = oModel.Body
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl);

                // Authenticate only if a username and password are provided
                if (!string.IsNullOrEmpty(_smtpSettings.Username) && !string.IsNullOrEmpty(_smtpSettings.Password))
                {
                    client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                }

                client.Send(message);
                client.Disconnect(true);
            }
        }




    }
}
