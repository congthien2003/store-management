using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IOrderDetailRepository<TOrderDetail> where TOrderDetail : OrderDetail
    {
        Task<TOrderDetail> CreateAsync(OrderDetail orderDetail);
        Task<TOrderDetail> UpdateAsync(OrderDetail orderDetail);
        Task<TOrderDetail> DeleteAsync(int idOrder, int idFood);
        Task<List<TOrderDetail>> GetAllByIdOrderAsync(int idOrder, int currentPage = 1, int pageSize = 5, string sortCol = "", bool ascSort = true);
        Task<int> GetCountAsync(int idOrder);
        Task<List<TOrderDetail>> GetByOrderIdAsync(int idOrder);
        Task<TOrderDetail> GetByOrderIdAndFoodIdAsync(int orderId, int foodId);
        Task<bool> ExistsAsync(int orderId, int foodId);
         


    }
}
