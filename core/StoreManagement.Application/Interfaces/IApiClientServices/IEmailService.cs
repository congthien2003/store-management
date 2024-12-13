using MimeKit.Tnef;
using StoreManagement.Application.DTOs.ApiClient.AWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.ApiClient
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
        Task<bool> SendEmailWithAttachmentAsync(EmailAttachment emailAttachment);
        Task<bool> SendEmailWelcome(MailWelcome mailWelcome);
        Task<bool> SendMailMonthlyReport(EmailMonthlyReport emailMonthlyReport);
        Task<bool> SendMailThanks(string recipientEmail);
    }
}
