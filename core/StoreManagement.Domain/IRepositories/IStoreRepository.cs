using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IStoreRepository<TStore> where TStore : Store
    {
        Task<int> CountAsync(string searchTerm);
        Task<int> GetCount(string searchTerm = "", bool incluDeleted = false);
        Task<TStore> CreateAsync(TStore store);
        Task<TStore> UpdateAsync(TStore store, bool includeDeleted = false);
        Task<TStore> DeleteAsync(int id);
        Task<TStore> GetByIdAsync(int id, bool includeDeleted = false);
        Task<Store> GetByIdUserAsync(int idUser, bool includeDeleted = false);
        Task<List<TStore>> GetByNameAsync(string name, bool includeDeleted = false);
        Task<List<TStore>> GetAllAsync(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
    }
}
