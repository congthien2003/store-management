using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.ApiClient
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string recipientEmail, string subject, string body);
        Task<bool> SendEmailWithAttachmentAsync(string recipientEmail, string subject, string body, byte[] attachment, string attachmentName, string contentType);
        Task<bool> SendEmailWelcome(string recipientEmail, string name);
        Task<bool> SendMailMonthlyReport(string recipientEmail, int idStore, string startDate, string endDate);
    }
}
