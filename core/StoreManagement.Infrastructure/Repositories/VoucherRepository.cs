using StoreManagement.Infrastructure.Data;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using StoreManagement.Domain.IRepositories;

namespace StoreManagement.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository<Voucher>
    {
        private readonly DataContext _dataContext;

        public VoucherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false)
        {
            var voucher = _dataContext.Vouchers.Where(x => x.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                voucher = voucher.Where(t => t.Name.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                voucher = voucher.Where(t => t.IsDeleted == incluDeleted);
            }
            var searchFood = await voucher.ToListAsync();
            return searchFood.Count();
        }

        public async Task<Voucher> CreateAsync(Voucher voucher)
        {
            if (voucher == null)
            {
                throw new NullReferenceException("Vouncher không tồn tại");
            }
            var exitsVoucher = await _dataContext.Vouchers.FirstOrDefaultAsync(x=>x.Name == voucher.Name &&x.Discount == voucher.Discount);
            if (exitsVoucher != null) 
            {
                throw new InvalidOperationException("Voucher này đã tồn tại");
            }
            var newVoucher = await _dataContext.Vouchers.AddAsync(voucher);
            await _dataContext.SaveChangesAsync();
            return newVoucher.Entity;
        }

        public async Task<Voucher> DeleteAsync(int id)
        {
            var voucher = await _dataContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (voucher == null)
            {
                throw new KeyNotFoundException("Không tìm thấy voucher");
            }
            _dataContext.Vouchers.Remove(voucher);
            await _dataContext.SaveChangesAsync();
            return voucher;
        }

        public Task<List<Voucher>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var voucher = _dataContext.Vouchers.Where(x => x.IdStore ==  idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                voucher = voucher.Where(t => t.Name.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                voucher = voucher.Where(t => t.IsDeleted == incluDeleted);
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    voucher = voucher.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    voucher = voucher.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = voucher.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<Voucher> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var voucher = await _dataContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (voucher == null)
            {
                throw new KeyNotFoundException("Không tìm thấy voucher");
            }
            return voucher;
        }

        public async Task<List<Voucher>> GetByNameAsync(string name, bool incluDeleted = false)
        {
            var vouchers = await _dataContext.Vouchers.Where(x =>x.Name.Contains(name) && x.IsDeleted == incluDeleted).ToListAsync();
            if(vouchers.Count == 0)
            {
                throw new KeyNotFoundException("Không tìm thấy voucher");
            }
            return vouchers;
        }
        public Expression<Func<Voucher, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "name":
                    return x => x.Name;
                default:
                    return x => x.Id;
            }
        }
        public async Task<Voucher> UpdateAsync(int id, Voucher voucher, bool incluDeleted = false)
        {
            var voucherUpdate = await _dataContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (voucherUpdate == null)
            {
                throw new KeyNotFoundException("Không tìm thấy voucher");
            }
            voucherUpdate.Name = voucher.Name;
            voucherUpdate.Discount = voucher.Discount;
            _dataContext.Vouchers.Update(voucherUpdate);
            await _dataContext.SaveChangesAsync();
            return voucherUpdate;
        }
    }
}
