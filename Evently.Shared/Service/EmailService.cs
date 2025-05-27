using Evently.Shared.Service.InterfaceService;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace BackendEvently.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _IConfiguration;

        public EmailService(IConfiguration config)
        {
            _IConfiguration = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_IConfiguration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_IConfiguration["EmailSettings:SmtpServer"], int.Parse(_IConfiguration["EmailSettings:Port"]), true);
            await smtp.AuthenticateAsync(_IConfiguration["EmailSettings:Username"], _IConfiguration["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
