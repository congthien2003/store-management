using StoreManagement.Application.Common;
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
        Task<PaginationResult<List<OrderResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string ascSort = "true");
        Task<double> CaculateTotal(int id, bool incluDeleted = false);
    }
}
