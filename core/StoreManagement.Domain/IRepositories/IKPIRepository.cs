using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.IRepositories
{
    public interface IKPIRepository<TKPI> where TKPI : KPI
    {
        Task<KPI> Add(KPI newKPI);
        Task<KPI> Update(KPI newKPI);
        Task<KPI> Delete(int id);
        Task<KPI> GetByLastestMonth(int month, int year);

        Task<IEnumerable<KPI>> GetByLastestYear(int year);
    }
}
