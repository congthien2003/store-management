using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IVoucherRepository<TVoucher> where TVoucher : Voucher
    {
        Task<TVoucher> CreateAsync(TVoucher voucher);
        Task<TVoucher> UpdateAsync(int id, TVoucher voucher, bool incluDeleted = false);
        Task<TVoucher> DeleteAsync(int id);
        Task<TVoucher> GetByIdAsync(int id, bool incluDeleted = false);
        Task<List<TVoucher>> GetByNameAsync(string name, bool incluDeleted = false);
        Task<List<TVoucher>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
        Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false);
    }
}
