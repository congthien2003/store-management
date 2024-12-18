using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.IRepositories
{
    public interface IStaffRepository<TStaff> where TStaff : Staff
    {
        Task<TStaff> CreateAsync(TStaff staff);
        Task<TStaff> UpdateASync(int id, TStaff staff, bool incluDeleted = false);
        Task<TStaff> DeleteAsync(int id, bool incluDeleted = false);
        Task<TStaff> GetByIdAsync(int id, bool incluDeleted = false);
        Task<List<TStaff>> GetAllByIdStore(int idStore, bool incluDeleted = false, string searchTerm = "", string sortCol = "", bool ascSort = true, int? role = 2);
        Task<List<TStaff>> GetStaffByNameAsync(string name, int idStore, bool incluDeleted = false );
    }
    
}
