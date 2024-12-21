using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;

namespace StoreManagement.Infrastructure.Repositories
{
    public class ComboItemRepository : IComboItemRepository
    {
        private readonly DataContext _context;

        public ComboItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComboItem>> GetItemsByComboIdAsync(int comboId)
        {
            return await _context.ComboItems
                .Where(ci => ci.IdCombo == comboId)
                .ToListAsync();
        }

        public async Task<bool> AddComboItemByListId(List<int> foodId, int comboId)
        {
            try
            {
                var comboItems = foodId.Select(idFood => new ComboItem
                {
                    IdCombo = comboId,
                    IdFood = idFood
                }).ToList();

                await _context.ComboItems.AddRangeAsync(comboItems);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Lỗi trong quá trình tạo combo item");
            }

        }

        public async Task<ComboItem> AddComboItemAsync(ComboItem comboItem)
        {
            await _context.ComboItems.AddAsync(comboItem);
            await _context.SaveChangesAsync();
            return comboItem;
        }

        public async Task<bool> UpdateComboItemAsync(int id, ComboItem comboItem)
        {
            var exists = await _context.ComboItems.FirstOrDefaultAsync(ci => ci.IdCombo == id);
            if (exists == null)
            {
                throw new Exception("Không tìm thấy");
            }
            _context.ComboItems.Update(comboItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteComboItemAsync(int id)
        {
            var comboItem = await _context.ComboItems.FindAsync(id);
            if (comboItem == null) return false;

            _context.ComboItems.Remove(comboItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
