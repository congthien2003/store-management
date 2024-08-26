using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IFoodService
    {
        Task<FoodDTO> CreateAsync(FoodDTO foodDTO);
        Task<FoodDTO> UpdateAsync(int id,FoodDTO foodDTO);
        Task<bool> DeleteAsync(int id);
        Task<FoodDTO> GetByIdAsync(int id);
        Task<List<FoodDTO>> GetByNameAsync(int idStore, string name);
        Task<List<FoodDTO>> GetByIdCategoryAsync(int id);
        Task<List<FoodDTO>> GetAllByIdStoreAsync(int id,int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false);
    }
}
