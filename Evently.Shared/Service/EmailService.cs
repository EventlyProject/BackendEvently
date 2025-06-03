using Evently.Shared.Service.InterfaceService;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace BackendEvently.Service
{
    public class EmailService : IEmailService
    {
        // Holds the configuration instance for accessing email settings
        private readonly IConfiguration _IConfiguration;

        // Constructor to inject configuration dependency
        public EmailService(IConfiguration config)
        {
            // Assign the injected configuration to the private field
            _IConfiguration = config;
        }

        // Asynchronously sends an email using the provided parameters
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            // Create a new email message
            var email = new MimeMessage();

            // Set the sender's email address from configuration
            email.From.Add(MailboxAddress.Parse(_IConfiguration["EmailSettings:From"]));

            // Set the recipient's email address
            email.To.Add(MailboxAddress.Parse(toEmail));

            // Set the subject of the email
            email.Subject = subject;

            // Set the body of the email as HTML
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            // Create a new SMTP client for sending the email
            using var smtp = new SmtpClient();

            // Connect to the SMTP server using settings from configuration
            await smtp.ConnectAsync(
                _IConfiguration["EmailSettings:SmtpServer"],
                int.Parse(_IConfiguration["EmailSettings:Port"]),
                true // Use SSL
            );

            // Authenticate with the SMTP server using credentials from configuration
            await smtp.AuthenticateAsync(
                _IConfiguration["EmailSettings:Username"],
                _IConfiguration["EmailSettings:Password"]
            );

            // Send the email
            await smtp.SendAsync(email);

            // Disconnect from the SMTP server
            await smtp.DisconnectAsync(true);
        }
    }
}
