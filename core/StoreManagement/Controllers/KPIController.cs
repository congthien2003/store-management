using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request.KPI;
using StoreManagement.Application.DTOs.Response.KPI;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {
        private readonly IKPIService _kPIService;

        public KPIController(IKPIService kPIService)
        {
            _kPIService = kPIService;
        }

        [HttpGet("/GetByLastestMonth")]
        public async Task<ActionResult<Result>> GetByLastestMonth(int month, int year)
        {
            return Ok(Result<KPIResponse>.Success(await _kPIService.GetDataKpiByMonth(month, year), "Lấy thông tin thành công"));
        }

        [HttpGet("/GetByLastestYear")]
        public async Task<ActionResult<Result>> GetByLastestYear(int year)
        {
            return Ok(Result<List<KPIResponse>>.Success(await _kPIService.GetKPIByYear(year), "Lấy thông tin thành công"));
        }

        [HttpPost("/UpdateKPI")]
        public async Task<ActionResult<Result>> UpdateKPI(UpdateKPIRequest req)
        {

            var update = await _kPIService.UpdateDataKpi(req.totalRevenue, req.totalSales, req.month, req.year);

            return Ok(Result<KPIResponse>.Success(update, "Lấy thông tin thành công"));
        }
    }
}
