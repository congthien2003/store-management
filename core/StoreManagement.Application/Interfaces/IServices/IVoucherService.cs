using StoreManagement.Application.DTOs.Request;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IVoucherService
    {
        Task<VoucherDTO> CreateAsync(VoucherDTO voucherDTO);
        Task<VoucherDTO> UpdateAsync(int id,VoucherDTO voucherDTO);
        Task<bool> DeleteAsync(int id);
        Task<VoucherDTO> GetByIdAsync(int id);
        Task<List<VoucherDTO>> GetByNameAsync(string name);
        Task<List<VoucherDTO>> GetAllByIdStore(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true);
        Task<int> GetCountList(int idStore, string searchTerm = "");
    }
}
