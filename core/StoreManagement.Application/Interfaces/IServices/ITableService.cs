using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface ITableService
    {
        Task<TableDTO> CreateAsync(TableDTO tableDTO);
        Task<TableResponse> UpdateAsync(int id, TableDTO tableDTO);
        Task<bool> DeleteAsync(int id);
        Task<TableResponse> GetByIdAsync(int id);
        Task<TableResponse> GetByGuIdAsync(Guid id);

        Task<PaginationResult<List<TableResponse>>> GetAllByIdStore(int id, string currentPage = "1", string pageSize = "9", bool filter = false, bool status = false);
        Task<int> GetCountAsync(int id);
    }
}
