using StoreManagement.Infrastructure.ApiClient;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;
using System.Globalization;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Application.Interfaces.IServices;
namespace StoreManagement.Application.Services
{
    public class AwsSesEmailService : IEmailService
    {
        private readonly string _awsAccessKey;
        private readonly string _awsSecretKey;
        private readonly string _senderEmail;
        private readonly RegionEndpoint _region;
        private readonly IExportExcellService _exportExcellService;
        private readonly IStoreService _storeService;
        public AwsSesEmailService(string awsAccessKey, string awsSecretKey, string senderEmail, RegionEndpoint region, IExportExcellService exportExcellService, IStoreService storeService)
        {
            _awsAccessKey = awsAccessKey;
            _awsSecretKey = awsSecretKey;
            _senderEmail = senderEmail;
            _region = region;
            _exportExcellService = exportExcellService;
            _storeService = storeService;
        
        }
        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            using var client = new AmazonSimpleEmailServiceClient(_awsAccessKey, _awsSecretKey, _region);

            var sendRequest = new SendEmailRequest
            {
                Source = _senderEmail,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { recipientEmail }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content(body)
                    }
                }
            };

            var response = await client.SendEmailAsync(sendRequest);

            return true;

        }

        public async Task<bool> SendEmailWelcome(string recipientEmail, string name)
        {
            using var client = new AmazonSimpleEmailServiceClient(_awsAccessKey, _awsSecretKey, _region);
            var body = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Welcome Email</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        margin: 0;
                        padding: 0;
                    }}
                    .email-container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        border: 1px solid #e0e0e0;
                        border-radius: 8px;
                        overflow: hidden;
                    }}
                    .header {{
                        background-color: #3367d6;
                        color: #ffffff;
                        padding: 20px;
                        text-align: center;
                    }}
                    .content {{
                        padding: 20px;
                    }}
                    .content p {{
                        font-size: 16px;
                        line-height: 1.5;
                        color: #333333;
                    }}
                    .footer {{
                        background-color: #f1f1f1;
                        color: #888888;
                        text-align: center;
                        padding: 10px;
                        font-size: 14px;
                    }}
                    .footer a {{
                        color: #3367d6;
                        text-decoration: none;
                    }}
                </style>
            </head>
            <body>
                <div class='email-container'>
                    <div class='header'>
                        <h1>Welcome to Our Service!</h1>
                    </div>
                    <div class='content'>
                        <p>Hi {name},</p>
                        <p>Thank you for signing up with us! We're thrilled to have you on board.</p>
                        <p>Feel free to explore our platform and let us know if there's anything we can help you with.</p>
                        <p>We hope you have an amazing experience!</p>
                        <p>Best regards,</p>
                        <p><strong>Ngơ Ngơ</strong></p>
                        <hr style=""margin: 20px 0; border: 1px solid #ddd;"">
                        <p>Xin chào {name},</p>
                        <p>Cảm ơn bạn đã đăng ký với chúng tôi! Chúng tôi rất vui mừng chào đón bạn.</p>
                        <p>Hãy thoải mái khám phá nền tảng của chúng tôi và cho chúng tôi biết nếu có điều gì chúng tôi có thể hỗ trợ bạn.</p>
                        <p>Chúng tôi hy vọng bạn sẽ có một trải nghiệm tuyệt vời!</p>
                        <p>Trân trọng,</p>
                        <p><strong>Ngơ Ngơ</strong></p>
                    </div>
                    <div class='footer'>
                        <p>&copy; 2024 EasyOrderManagement. All rights reserved.</p>
                        <p><a href='#'>Privacy Policy</a> | <a href='#'>Contact Us</a></p>
                    </div>
                </div>
            </body>
            </html>";

            var subject = "Welcome to Our Store!";
            var sendRequest = new SendEmailRequest
            {
                Source = _senderEmail,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { recipientEmail }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content(body)
                    }
                }
            };

            var response = await client.SendEmailAsync(sendRequest);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> SendEmailWithAttachmentAsync(string recipientEmail, string subject, string body, byte[] attachment, string attachmentName, string contentType)
        {
            using var client = new AmazonSimpleEmailServiceClient(_awsAccessKey, _awsSecretKey, _region);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender", _senderEmail));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };


            if (attachment != null && !string.IsNullOrEmpty(attachmentName))
            {

                bodyBuilder.Attachments.Add(attachmentName, attachment, ContentType.Parse(contentType ?? "application/octet-stream"));
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var memoryStream = new MemoryStream();
            await message.WriteToAsync(memoryStream);
            var rawMessage = new RawMessage
            {
                Data = memoryStream
            };

            var request = new SendRawEmailRequest
            {
                RawMessage = rawMessage
            };

            var response = await client.SendRawEmailAsync(request);

            return true;
        }
        public async Task<bool> SendMailMonthlyReport(string recipientEmail,int idStore, string startDate, string endDate)
        {
            DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var excelContent = await _exportExcellService.ExportFoodSalesToExcel(idStore, start, end);
            var store = await _storeService.GetByIdAsync(idStore);
            var storeName = store.Name;
            string subject = $"Monthly Report: {storeName} ({startDate} - {endDate})";
            string body = $@"
            <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <p>Dear {storeName} Store,</p>
                <p>Attached is the monthly sales report for your store from <strong>{startDate}</strong> to <strong>{endDate}</strong>.</p>
                <p>If you have any questions or need further assistance, please let us know.</p>
                <p>Best regards,</p>
                <p><strong>Ngơ Ngơ<strong></p>
            </div>";
            string attachmentName = $"Monthly_Report_{storeName.Replace(" ", "_")}_{startDate}_to_{endDate}.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return await SendEmailWithAttachmentAsync(recipientEmail, subject, body, excelContent, attachmentName, contentType);
        }
    }
}
