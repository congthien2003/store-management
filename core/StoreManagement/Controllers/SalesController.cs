using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Interfaces.IApiClientServices;
using System.Globalization;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IExportExcellService _exportExcellService;

        public SalesController(IExportExcellService exportExcellService)
        {
            _exportExcellService = exportExcellService;
        }

        [HttpGet("export-excel")]
        public async Task<ActionResult> ExportFoodSalesToExcell(int idStore, string startDateStr, string endDateStr)
        {
            DateTime startDate = DateTime.ParseExact(startDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var content = await _exportExcellService.ExportFoodSalesToExcel(idStore, startDate, endDate);

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FoodSalesReport.xlsx");
        }

    }
}
