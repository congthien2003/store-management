using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateAsync(int id, CategoryDTO categoryDTO);
        Task<bool> DeleteAsync(int id);
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<List<CategoryResponse>>GetByIdStore(int id);
        Task<List<CategoryResponse>> GetByNameAsync(int idStore, string name);
        Task<List<CategoryResponse>> GetAllByIdStoreAsync(int id, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountList(int idStore,string searchTerm = "", bool incluDeleted = false);
    }
}
