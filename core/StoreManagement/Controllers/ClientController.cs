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

        /*[HttpPost("get-qr")]
        public async Task<ActionResult> GetQR(QRRequest req)
        {
            return Ok(Result<QRResponse>.Success(await qRServices.GetQR(req.BankId, req.AccountNo, req.AccountName, req.Amount), "Success"));
        }*/
    }
}
