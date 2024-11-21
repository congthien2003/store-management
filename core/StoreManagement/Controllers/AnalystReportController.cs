using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Response.Analyst;
using StoreManagement.Application.Interfaces.IApiClientServices;
using System.Globalization;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalystReportController : ControllerBase
    {
        private readonly IAnalystReportService _analystReportService;

        public AnalystReportController(IAnalystReportService analystReportService)
        {
            _analystReportService = analystReportService;
        }
        [HttpGet("count-food/{idStore:int}")]
        public async Task<ActionResult<Result>> GetCountFoodSaleByDay(int idStore, string dateTime)
        {
            DateTime date = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var count = await _analystReportService.GetCountFoodSaleInDay(idStore, date);
            return Ok(Result<int>.Success(count,"Tính toán thành công"));
        }
        [HttpGet("count-order/{idStore:int}")]
        public async Task<ActionResult<Result>> GetCountOrderByDay(int idStore, string dateTime)
        {
            DateTime date = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var count = await _analystReportService.GetCountOrderInDay(idStore, date);
            return Ok(Result<int>.Success(count, "Tính toán thành công"));
        }
        [HttpGet("table-free/{idStore:int}")]
        public async Task<ActionResult<Result>> GetTableFree(int idStore)
        {
            var count = await _analystReportService.GetTableFree(idStore);
            return Ok(Result<int>.Success(count, "Tính toán thành công"));
        }
        [HttpGet("daily-revenue/{idStore:int}")]
        public async Task<ActionResult<Result>> GetDailyRevenueInDay(int idStore, string dateTime)
        {
            DateTime date = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var total = await _analystReportService.GetDailyRevenueInDay(idStore, date);
            return Ok(Result<double>.Success(total, "Tính toán thành công")); ;   
        }
        [HttpGet("month-revenue/{idStore:int}/{year:int}")]
        public async Task<ActionResult<Result>> GetMonthRevenue(int idStore, int year)
        {
            var list = await _analystReportService.GetMonthRevenue(idStore, year);
            return Ok(Result<List<RevenueByMonth>>.Success(list, "Tính toán thành công")); ;
        }
        [HttpGet("month-order/{idStore:int}/{year:int}")]
        public async Task<ActionResult<Result>> GetMonthOrder(int idStore, int year)
        {
            var list = await _analystReportService.GetMonthOrder(idStore, year);
            return Ok(Result<List<OrderByMonth>>.Success(list, "Tính toán thành công")); ;
        }
    }
}
