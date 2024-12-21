using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IComboItemRepository
    {
        Task<IEnumerable<ComboItem>> GetItemsByComboIdAsync(int comboId);
        Task<bool> AddComboItemByListId(List<int> foodId, int comboId);
        Task<ComboItem> AddComboItemAsync(ComboItem comboItem);
        Task<bool> UpdateComboItemAsync(int id, ComboItem comboItem);
        Task<bool> DeleteComboItemAsync(int id);
    }
}
