using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface IAnalystReportService
    {
        Task<int> GetCountOrderInDay(int idStore, DateTime dateTime);
        Task<int> GetTableFree(int idStore);
        Task<int> GetCountFoodSaleInDay(int idStore, DateTime dateTime);
        Task<double> GetDailyRevenueInDay(int idStore, DateTime dateTime);  
        
    }
}
