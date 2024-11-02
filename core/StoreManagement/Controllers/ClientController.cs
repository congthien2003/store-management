using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.ApiClient.QR;
using StoreManagement.Application.Interfaces.IApiClientServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IQRServices qRServices;
        public ClientController(IQRServices qRServices)
        {
            this.qRServices = qRServices;
        }

        [HttpGet("get-qr")]
        public async Task<ActionResult> GetQR(string code)
        {
            return Ok(Result<QRResponse>.Success(await qRServices.GetQR("1", "ABC", "1", 5000), "Success"));
        }
    }
}
