using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IStaffService
    {
        Task<StaffDTO> CreateAsync(StaffDTO staffDTO);
        Task<StaffDTO> UpdateAsync(int id,StaffDTO staffDTO);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<StaffResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, int? categoryId = null);
        Task<List<StaffResponse>> GetByNameAsync(string name,int idStore);
        Task<StaffResponse> GetByIdAsync(int id);

    }
}
