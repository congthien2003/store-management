using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IVoucherService
    {
        Task<VoucherDTO> CreateAsync(VoucherDTO voucherDTO);
        Task<VoucherDTO> UpdateAsync(int id,VoucherDTO voucherDTO);
        Task<bool> DeleteAsync(int id);
        Task<VoucherResponse> GetByIdAsync(int id);
        Task<List<VoucherResponse>> GetByNameAsync(string name);
        Task<PaginationResult<List<VoucherResponse>>> GetAllByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool ascSort = true);
        Task<int> GetCountList(int idStore, string searchTerm = "");
    }
}
