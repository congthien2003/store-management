using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StoreManagement.Infrastructure.Repositories
{
    public class KPIRepository : IKPIRepository<KPI>
    {
        private readonly DataContext _dataContext;
        public KPIRepository(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<KPI> Add(KPI newKPI)
        {
            var kpi = await _dataContext.KPIs.AddAsync(newKPI);
            await _dataContext.SaveChangesAsync();
            return kpi.Entity;
        }

        public async Task<KPI> Delete(int id)
        {
            var kpi = await _dataContext.KPIs.FirstOrDefaultAsync(x => x.Id == id);
            if (kpi == null)
            {
                throw new InvalidOperationException("KPI không tồn tại");
            }
            _dataContext.KPIs.Remove(kpi);
            await _dataContext.SaveChangesAsync();
            return kpi;
        }

        public async Task<KPI> GetByLastestMonth(int month, int year)
        {
            var kpi = await _dataContext.KPIs.FirstOrDefaultAsync(x => x.Month == month && x.Year == year);
            return kpi ?? null;
        }

        public async Task<IEnumerable<KPI>> GetByLastestYear(int year)
        {
            var kpi = _dataContext.KPIs.Where(x => x.Year == year).OrderBy(x => x.Month).AsEnumerable();
            return kpi ?? null;
        }

        public async Task<KPI> Update(KPI kpiUpdate)
        {
            var kpi = await _dataContext.KPIs.FirstOrDefaultAsync(x => x.Id == kpiUpdate.Id);
            if (kpi == null)
            {
                throw new InvalidOperationException("KPI này không có");
            }
            _dataContext.KPIs.Update(kpiUpdate);
            await _dataContext.SaveChangesAsync();
            return kpiUpdate;
        }
    }
}
