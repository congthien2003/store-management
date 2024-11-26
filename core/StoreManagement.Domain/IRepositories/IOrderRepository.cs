using StoreManagement.Domain.Models;
using System.Runtime.CompilerServices;

namespace StoreManagement.Domain.IRepositories
{
    public interface IOrderRepository<TOrder> where TOrder : Order
    {
        Task<TOrder> CreateAsync(TOrder order);
        Task<TOrder> UpdateAsync(int id, TOrder order, bool incluDeleted = false);
        Task<TOrder> DeleteAsync(int id, bool incluDeleted = false);
        Task<TOrder> GetByIdAsync(int id, bool incluDeleted = false);
        Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false);
        Task<List<TOrder>> GetAllByIdStoreAsync(int idStore, string sortCol = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountOrderInDay(int idStore, DateTime date, bool incluDeleted = false);
        Task<int> GetMonthOrderAsync(int idStore, int month, int year, bool incluDeleted = false);
        Task<int> GetDailyFoodSaleAsync(int idStore, DateTime dateTime, bool incluDeleted = false);
        Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<bool> CheckOrderDetailExists(int orderId, int foodId);
        Task<int> GetMonthFoodAsync(int idStore,int month ,int year, bool incluDeleted = false);
        Task<double> GetAVGFoodPerOrderOneMonthAsync(int idStore, int month, int year, bool incluDeleted = false);

    }
}
