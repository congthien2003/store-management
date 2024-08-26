using StoreManagement.DataAccess.Data;
using StoreManagement.Domain.Interfaces.IRepositories;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StoreManagement.Repositories
{
    public class OrderRepositoy : IOrderRepository<Order>
    {
        private readonly DataContext _dataContext;

        public OrderRepositoy(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            var newOrder = await _dataContext.Orders.AddAsync(order);
            if (newOrder == null)
            {
                throw new ArgumentException("Tạo order không thành công");
            }
            await _dataContext.SaveChangesAsync();
            return newOrder.Entity;
        }

        public async Task<Order> DeleteAsync(int id, bool incluDeleted = false)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (order == null)
            {
                throw new KeyNotFoundException("Order không tồn tại");
            }
            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }

        public Task<List<Order>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var order = _dataContext.Orders.Where(x => x.Table.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                order = order.Where(t => t.NameUser.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                order = order.Where(t => t.IsDeleted == incluDeleted);
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    order = order.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    order = order.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = order.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<Order> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (order == null)
            {
                throw new KeyNotFoundException("Order không tồn tại");
            }
            return order;
        }

        public async Task<List<Order>> GetByNameUser(string name, bool incluDeleted = false)
        {
            var listOrders = await _dataContext.Orders.Where(x => x.NameUser.Contains(name) && x.IsDeleted == incluDeleted).ToListAsync();
            if(listOrders.Count == 0)
            {
                throw new InvalidOperationException("Tên khách hàng order không tồn tại");
            }
            return listOrders;
        }

        public async Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false)
        {
            var order = _dataContext.Orders.Where(x => x.Table.IdStore ==  idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                order = order.Where(t => t.NameUser.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                order = order.Where(t => t.IsDeleted == incluDeleted);
            }
            var searchFood = await order.ToListAsync();
            return searchFood.Count();
        }
        public Expression<Func<Order, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "name":
                    return x => x.NameUser;
                default:
                    return x => x.Id;
            }
        }
        public async Task<Order> UpdateAsync(int id, Order order, bool incluDeleted = false)
        {
            var orderUpdate = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if(orderUpdate == null)
            {
                throw new KeyNotFoundException("Order không tồn tại");
            }
            orderUpdate.Total = order.Total;
            orderUpdate.IdTable = order.IdTable;
            orderUpdate.CreatedAt = order.CreatedAt;
            orderUpdate.NameUser = order.NameUser;
            orderUpdate.PhoneUser = orderUpdate.PhoneUser;
            _dataContext.Orders.Update(orderUpdate);
            await _dataContext.SaveChangesAsync();
            return orderUpdate;
        }
    }
}
