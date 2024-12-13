using StoreManagement.Application.DTOs.Request.ComboItem;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IComboItemService
    {
        Task<List<ComboItemDTO>> CreateByListIdFood(int comboId, int[] listIdFood);
        Task<ComboItemDTO> CreateAsync(ComboItemDTO comboItemDTO);
        Task<ComboItemDTO> UpdateAsync(int id, ComboItemDTO comboItemDTO);
        Task<bool> DeleteAsync(int id);
        Task<List<ComboItemDTO>> GetByComboIdAsync(int comboId);
    }
}
