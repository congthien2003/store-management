using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IStoreService
    {
        Task<StoreDTO> UpdateAsync(int id,StoreDTO storeDTO);
        Task<bool> DeleteAsync(int id);
        Task<StoreDTO> CreateAsync(StoreDTO storeDTO);
        Task<StoreResponse> GetByIdAsync(int id);
        Task<List<StoreResponse>> GetByNameAsync(string name);
        Task<List<StoreResponse>> GetAllAsync(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountList(string searchTerm = "", bool incluDeleted = false);
    }
}
