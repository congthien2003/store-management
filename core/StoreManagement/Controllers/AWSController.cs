using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.ApiClient.AWS;
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
        public async Task<ActionResult<Result>> SendEmail(EmailRequest emailRequest)
        {
            var result = await _emailService.SendEmailAsync(emailRequest);
            return Ok(Result<bool>.Success(result,"Gửi thư thành công"));
        }
        [HttpPost("send-mail-attachment")]
        public async Task<ActionResult<Result>> SendEmailAttachment(EmailAttachment emailAttachment)
{
    if (emailAttachment?.Attachment != null && emailAttachment.Attachment.Length > 0)
    {
        using (var memoryStream = new MemoryStream(emailAttachment.Attachment))
        {
            
            var updatedAttachment = new MemoryStream();
            await memoryStream.CopyToAsync(updatedAttachment);
            emailAttachment.Attachment = updatedAttachment.ToArray(); 
        }
    }
    else
    {
        return BadRequest(Result<bool>.Failure("Tệp đính kèm không hợp lệ"));
    }

    var result = await _emailService.SendEmailWithAttachmentAsync(emailAttachment);

    if (result)
    {
        return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
    }
    else
    {
        return StatusCode(500, Result<bool>.Failure("Gửi thư không thành công"));
    }
}



        [HttpPost("send-welcome")]
        public async Task<ActionResult<Result>> SendEmailWelcome(MailWelcome mailWelcome)
        {
            var result = await _emailService.SendEmailWelcome(mailWelcome);
            return Ok(Result<bool>.Success(result,"Gửi thư thành công"));
        }
        [HttpPost("send-monthly-report")]
        public async Task<ActionResult<Result>> SendEmailMonthlyReport(EmailMonthlyReport emailMonthlyReport)
        {
            var result = await _emailService.SendMailMonthlyReport(emailMonthlyReport);
            return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
        }
        [HttpPost("send-mail-thanks")]
        public async Task<ActionResult<Result>> SendEmailThanks([FromBody]string recipent)
        {
            var result = await _emailService.SendMailThanks(recipent);
            return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
        }
    }
}
