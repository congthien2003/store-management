using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IFoodService
        {
                Task<FoodDTO> CreateAsync(FoodDTO foodDTO);
                Task<FoodDTO> UpdateAsync(int id, FoodDTO foodDTO);
                Task<bool> DeleteAsync(int id);
                Task<FoodResponse> GetByIdAsync(int id);
                Task<List<FoodResponse>> GetByNameAsync(int idStore, string name);
                Task<List<FoodResponse>> GetByIdCategoryAsync(int id);
                Task<PaginationResult<List<FoodResponse>>> GetAllByIdStoreAsync(int id, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string ascSort = "true", bool incluDeleted = false);
                Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false);
                Task<FoodBestSellerResponse> GetTopFood(int idStore, int idCategory, int currentPage, int pageSize);
        }
}
