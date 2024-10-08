using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
        public interface ICategoryService
        {
                Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
                Task<CategoryDTO> UpdateAsync(int id, CategoryDTO categoryDTO);
                Task<bool> DeleteAsync(int id);
                Task<CategoryResponse> GetByIdAsync(int id);
                Task<List<CategoryResponse>> GetByNameAsync(int idStore, string name);
                Task<PaginationResult<List<CategoryResponse>>> GetAllByIdStoreAsync(int id, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string ascSort = "true", bool incluDeleted = false);
                Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false);
        }
}
