using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
        public interface IOrderSerivce
        {
                Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
                Task<OrderDTO> UpdateAsync(int id, OrderDTO orderDTO);
                Task<bool> DeleteAsync(int id);
                Task<OrderResponse> GetByIdAsync(int id);
                Task<int> GetCountAsync(int idStore, string searchTerm = "");
                Task<List<OrderResponse>> GetByNameUserAsync(string name);
                Task<List<OrderResponse>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true);
        }
}
