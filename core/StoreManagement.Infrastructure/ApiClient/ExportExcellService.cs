
using ClosedXML.Excel;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Application.Interfaces.IServices;
namespace StoreManagement.Infrastructure.Services
{
    public class ExportExcellService : IExportExcellService
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IStoreService _storeService;

        public ExportExcellService(IOrderDetailService orderDetailService, IStoreService storeService)
        {
            _orderDetailService = orderDetailService;
            _storeService = storeService;
        }

        public async Task<byte[]> ExportFoodSalesToExcel(int idStore, DateTime startDate, DateTime endDate)
        {
            var foodSalesReport = await _orderDetailService.GetFoodSalesReport(idStore, startDate, endDate);
            var store = await _storeService.GetByIdAsync(idStore);
            var storeName = store.Name;

            int totalQuantitySold = 0;
            int totalFoods = foodSalesReport.Count();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Food Sales Report");
            worksheet.Cell(1, 1).Value = "Thống kê của " + storeName;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 14;
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(2, 1).Value = "Ngày bắt đầu:";
            worksheet.Cell(2, 2).Value = startDate.ToString("dd/MM/yyyy");
            worksheet.Cell(2, 1).Style.Font.Bold = true;

            worksheet.Cell(3, 1).Value = "Ngày kết thúc:";
            worksheet.Cell(3, 2).Value = endDate.ToString("dd/MM/yyyy");
            worksheet.Cell(3, 1).Style.Font.Bold = true;

            worksheet.Cell(5, 1).Value = "Tên món ăn";
            worksheet.Cell(5, 2).Value = "Số lượng bán";

            var headerRange = worksheet.Range(5, 1, 5, 2);
            headerRange.Style.Fill.SetBackgroundColor(XLColor.CornflowerBlue);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            headerRange.Style.Font.FontColor = XLColor.White;

            int row = 6;
            foreach (var item in foodSalesReport)
            {
                worksheet.Cell(row, 1).Value = item.Key;
                worksheet.Cell(row, 2).Value = item.Value;

                // Tính tổng số lượng bán được
                totalQuantitySold += item.Value;

                worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row++;
            }

            // Định dạng các ô dữ liệu
            var dataRange = worksheet.Range(6, 1, row - 1, 2);
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Thêm dòng tổng ở cuối bảng
            worksheet.Cell(row, 1).Value = "Tổng cộng số món:";
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            worksheet.Cell(row, 2).Value = totalFoods;
            worksheet.Cell(row, 2).Style.Font.Bold = true;
            worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            row++;

            worksheet.Cell(row, 1).Value = "Tổng Số lượng bán:";
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            worksheet.Cell(row, 2).Value = totalQuantitySold;
            worksheet.Cell(row, 2).Style.Font.Bold = true;
            worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
