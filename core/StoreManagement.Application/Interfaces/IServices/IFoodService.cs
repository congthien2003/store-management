using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IFoodService
    {
        Task<FoodDTO> CreateAsync(FoodDTO foodDTO);
        Task<FoodDTO> UpdateAsync(int id,FoodDTO foodDTO);
        Task<bool> DeleteAsync(int id);
        Task<FoodResponse> GetByIdAsync(int id);
        Task<List<FoodResponse>> GetByNameAsync(int idStore, string name);
        Task<List<FoodResponse>> GetByIdCategoryAsync(int id);
        Task<List<FoodResponse>> GetAllByIdStoreAsync(int id,int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false);
    }
}
