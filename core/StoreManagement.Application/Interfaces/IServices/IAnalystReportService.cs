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

    }
}
