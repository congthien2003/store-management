using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IOrderSerivce
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
        Task<OrderDTO> UpdateAsync(int id, OrderDTO orderDTO);
        Task<bool> DeleteAsync(int id);
        Task<OrderDTO> GetByIdAsync(int id);
        Task<int> GetCountAsync(int idStore, string searchTerm = "");
        Task<List<OrderDTO>> GetByNameUserAsync(string name);
        Task<double> CaculateTotal(int id, bool incluDeleted = false);
        Task<PaginationResult<List<OrderDTO>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string ascSort = "true");
    }
}
