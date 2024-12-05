using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Infrastructure.ApiClient;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWSController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public AWSController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("send-mail")]
        public async Task<ActionResult<Result>> SendEmail(string recipient, string subject, string body)
        {
            var result = await _emailService.SendEmailAsync(recipient, subject, body);
            return Ok(Result<bool>.Success(result,"Gửi thư thành công"));
        }
        [HttpPost("send-mail-attachment")]
        public async Task<ActionResult<Result>> SendEmailAttachment(string recipient, string subject, string body, IFormFile attachment)
        {
            byte[] attachmentBytes = null;
            string attachmentName = null;
            string contentType = null;

            if (attachment != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await attachment.CopyToAsync(memoryStream);
                    attachmentBytes = memoryStream.ToArray();
                    attachmentName = attachment.FileName;
                    contentType = attachment.ContentType; 
                }
            }

            var result = await _emailService.SendEmailWithAttachmentAsync(recipient, subject, body, attachmentBytes, attachmentName, contentType);
            return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
        }
        [HttpPost("send-welcome")]
        public async Task<ActionResult<Result>> SendEmailWelcome(string recipient, string name)
        {
            var result = await _emailService.SendEmailWelcome(recipient, name);
            return Ok(Result<bool>.Success(result,"Gửi thư thành công"));
        }
        [HttpPost("send-monthly-report")]
        public async Task<ActionResult<Result>> SendEmailMonthlyReport(string recipient, int idStore, string startDate, string endDate)
        {
            var result = await _emailService.SendMailMonthlyReport(recipient,idStore, startDate,endDate);
            return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
        }
    }
}
