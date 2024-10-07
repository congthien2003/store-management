using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
        public interface IStoreService
        {
                Task<StoreDTO> UpdateAsync(StoreDTO storeDTO);
                Task<bool> DeleteAsync(int id);
                Task<StoreDTO> CreateAsync(StoreDTO storeDTO);
                Task<StoreResponse> GetByIdAsync(int id);
                Task<List<StoreResponse>> GetByNameAsync(string name);
                Task<PaginationResult<List<StoreResponse>>> GetAllAsync(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true");
                Task<int> GetCountList(string searchTerm = "", bool incluDeleted = false);
        }
}
