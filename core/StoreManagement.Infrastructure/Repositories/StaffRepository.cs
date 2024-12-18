using AutoMapper;
using StoreManagement.Infrastructure.Data;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using StoreManagement.Domain.IRepositories;
using Mscc.GenerativeAI;

namespace StoreManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository<Staff>
    {
        private readonly DataContext _dataContext;

        public StaffRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Staff> CreateAsync(Staff staff)
        {
            var store = await _dataContext.Stores.FirstOrDefaultAsync(x => x.Id == staff.IdStore && x.IsDeleted == false);
            if (store == null)
            {
                throw new NullReferenceException("Không tồn tại cửa hàng");
            }
            
            var newStaff = await _dataContext.Staffs.AddAsync(staff);
            await _dataContext.SaveChangesAsync();
            return newStaff.Entity;
        }

        public async Task<Staff> DeleteAsync(int id, bool incluDeleted = false)
        {
            var staff = await _dataContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (staff == null)
            {
                throw new NullReferenceException("Không tìm thấy thể loại cần xóa");
            }
            staff.IsDeleted = true;
            _dataContext.Staffs.Update(staff);
            await _dataContext.SaveChangesAsync();   
            return staff;
        }

        public async Task<List<Staff>> GetAllByIdStore(int idStore, bool incluDeleted = false, string searchTerm = "", string sortCol = "", bool ascSort = true, int? role = 2)
        {
            var query = _dataContext.Staffs.Where(x => x.IdStore == idStore).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.Name.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                query = query.Where(t => t.IsDeleted == incluDeleted);
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    query = query.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    query = query.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = query.ToListAsync();
            return await list;
        }

        public async Task<Staff> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var staff = await _dataContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (staff == null)
            {
                throw new KeyNotFoundException("Không tìm thấy nhân viên");
            }
            return staff;
        }

        public async Task<List<Staff>> GetStaffByNameAsync(string name,int idStore, bool incluDeleted = false)
        {
            var listStaffs = await _dataContext.Staffs.Where(x => x.Name.Contains(name) && x.IsDeleted == incluDeleted && x.Store.Id == idStore).ToListAsync();
            if (listStaffs.Count == 0)
            {
                throw new KeyNotFoundException("Thể loại không tồn tại");
            }
            return listStaffs;
        }
        public Expression<Func<Staff, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "name":
                    return x => x.Name;
                default:
                    return x => x.Id;
            }
        }
        public async Task<Staff> UpdateASync(int id, Staff staff, bool incluDeleted = false)
        {
            var staffUpdate = await _dataContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (staffUpdate == null)
            {
                throw new KeyNotFoundException("Không tìm thấy thể loại để cập nhật");
            }
            _dataContext.Staffs.Update(staff);
            await _dataContext.SaveChangesAsync();
            return staff;
        }
    }
}
