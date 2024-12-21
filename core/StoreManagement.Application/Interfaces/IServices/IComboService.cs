using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request.Combo;
using StoreManagement.Application.DTOs.Response.Combo;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IComboService
    {
        Task<ComboDTO> CreateAsync(CreateComboReq combo);
        Task<ComboDTO> UpdateAsync(int id, ComboDTO comboDTO);
        Task<bool> DeleteAsync(int id);
        Task<ComboDTO> GetByIdAsync(int id);
        Task<PaginationResult<List<ComboWithFood>>> GetAllAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false);
    }
}
