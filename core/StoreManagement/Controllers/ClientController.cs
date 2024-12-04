using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.ApiClient.Chat;
using StoreManagement.Application.DTOs.ApiClient.QR;
using StoreManagement.Application.Interfaces.IApiClientServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IQRServices qRServices;
        private readonly IGoogleAPI googleAPI;
        public ClientController(IQRServices qRServices, IGoogleAPI googleAPI)
        {
            this.qRServices = qRServices;
            this.googleAPI = googleAPI;
        }

        [HttpPost("ChatGemini")]
        public async Task<dynamic> ChatBox(ChatReq req)
        {
            var result = await googleAPI.Gemini(req.promt);
            return Ok(Result<dynamic>.Success(result, "Success"));
        }
    }
}
