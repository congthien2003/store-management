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
        Task<List<OrderDTO>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true);
    }
}
