using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IComboRepository
    {
        Task<Combo> AddComboAsync(Combo combo);
        Task<bool> UpdateComboAsync(int id, Combo combo);
        Task<bool> DeleteComboAsync(int id);
        Task<Combo> GetComboByIdAsync(int id);
        Task<IEnumerable<Combo>> GetAllByStoreAsync(int idStore, string searchTerm = "", string sortColumn = "", bool asc = false);
    }
}
