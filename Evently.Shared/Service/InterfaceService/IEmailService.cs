using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    // Interface for email service functionality
    public interface IEmailService
    {
        // Sends an email asynchronously to the specified address with the given subject and message
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
