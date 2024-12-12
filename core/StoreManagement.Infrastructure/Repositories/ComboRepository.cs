using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreManagement.Infrastructure.Repositories
{
    public class ComboRepository : IComboRepository
    {
        private readonly DataContext _context;

        public ComboRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Combo> GetComboByIdAsync(int id)
        {
            return await _context.Combos.FindAsync(id);
        }

        public async Task<Combo> AddComboAsync(Combo combo)
        {
            await _context.Combos.AddAsync(combo);
            await _context.SaveChangesAsync();
            return combo;
        }

        public async Task<bool> UpdateComboAsync(int id, Combo combo)
        {
            var exists = await _context.Combos.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                throw new Exception("Không tìm thấy");
            }
            _context.Combos.Update(combo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteComboAsync(int id)
        {
            var combo = await GetComboByIdAsync(id);
            if (combo == null) return false;

            _context.Combos.Remove(combo);
            return await _context.SaveChangesAsync() > 0;
        }

        public Expression<Func<Combo, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "price":
                    return x => x.Price;
                case "name":
                    return x => x.Name;
                default:
                    return x => x.Id;
            }
        }

        public async Task<IEnumerable<Combo>> GetAllByStoreAsync(int idStore, string searchTerm = "", string sortColumn = "", bool asc = false)
        {
            var combo = _context.Combos.
                Include(c => c.ComboItems)
                .ThenInclude(ci => ci.Food).Where(x => x.IdStore == idStore).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                combo = combo.Where(t => t.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (asc)
                {
                    combo = combo.OrderByDescending(GetSortColumnExpression(sortColumn.ToLower()));
                }
                else
                {
                    combo = combo.OrderBy(GetSortColumnExpression(sortColumn.ToLower()));

                }
            }
            return await combo.ToListAsync();
        }
    }
}
