using StoreManagement.Application.DTOs.Response.Analyst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IAnalystReportService
    {
        Task<int> GetCountOrderInDay(int idStore, DateTime dateTime);
        Task<int> GetTableFree(int idStore);
        Task<int> GetCountFoodSaleInDay(int idStore, DateTime dateTime);
        Task<double> GetDailyRevenueInDay(int idStore, DateTime dateTime);
        Task<List<RevenueByMonth>> GetMonthRevenue(int idStore, int year);
        Task<List<OrderByMonth>> GetMonthOrder(int idStore, int year);
        Task<List<DataByMonth>> GetTotalProductSell(int idStore, int year);
        Task<List<FoodByMonth>> GetMonthFood(int idStore, int year);
        Task<AvgFoodOneMonth> GetAvgFoodPerOrderOneMonth(int idStore, int month, int year);
        Task<List<MonthlyReport>> monthlyReports(int idStore);
        Task<List<MonthlySummaryReport>> monthlySummaryReports(int idStore);

    }
}
