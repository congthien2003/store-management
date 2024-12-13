using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreManagement.Infrastructure.Repositories
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

        public Task<List<Order>> GetAllByIdStoreAsync(int idStore, string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var order = _dataContext.Orders.Include("Table").Where(x => x.Table.IdStore == idStore && x.IsDeleted == incluDeleted).OrderByDescending(t => t.CreatedAt).AsQueryable();
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
            var list = order.ToListAsync();
            return list;
        }

        public Task<int> GetCountOrderInDay(int idStore, DateTime date, bool incluDeleted = false)
        {
            var order = _dataContext.Orders.Include("Table").Where(x => x.Table.IdStore == idStore && x.IsDeleted == incluDeleted && x.CreatedAt.Date >= date.Date && x.CreatedAt.Date <= DateTime.Now.Date).AsQueryable();
            if (!incluDeleted)
            {
                order = order.Where(t => t.IsDeleted == incluDeleted);
            }
            var countOrder = order.CountAsync();
            return countOrder;
        }

        public async Task<Order> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var order = await _dataContext.Orders.Include("Table").FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (order == null)
            {
                throw new KeyNotFoundException("Order không tồn tại");
            }
            return order;
        }

        public async Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false)
        {
            var order = _dataContext.Orders.Where(x => x.Table.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!incluDeleted)
            {
                order = order.Where(t => t.IsDeleted == incluDeleted);
            }
            var searchFood = await order.ToListAsync();
            return searchFood.Count();
        }

        public async Task<int> GetDailyFoodSaleAsync(int idStore, DateTime dateTime, bool includeDeleted = false)
        {
            var orders = await _dataContext.Orders
                                .Where(x => x.Table.IdStore == idStore && x.CreatedAt.Date == dateTime.Date && x.IsDeleted == includeDeleted)
                                .Include(x => x.OrderDetails)
                                .ToListAsync();
            int totalQuantity = orders.SelectMany(x => x.OrderDetails).Sum(x => x.Quantity);
            return totalQuantity;
        }

        public Expression<Func<Order, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {

                default:
                    return x => x.Id;
            }
        }
        public async Task<Order> UpdateAsync(int id, Order order, bool incluDeleted = false)
        {
            var orderUpdate = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (orderUpdate == null)
            {
                throw new KeyNotFoundException("Order không tồn tại");
            }
            orderUpdate.Total = order.Total;
            orderUpdate.IdTable = order.IdTable;
            orderUpdate.CreatedAt = order.CreatedAt;
            _dataContext.Orders.Update(orderUpdate);
            await _dataContext.SaveChangesAsync();
            return orderUpdate;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _dataContext.OrderDetails
                .Where(od => od.IdOrder == orderId)
                .ToListAsync();
        }

        public async Task<bool> CheckOrderDetailExists(int orderId, int foodId)
        {
            return await _dataContext.OrderDetails
           .AnyAsync(od => od.IdOrder == orderId && od.IdFood == foodId);
        }
        public async Task<int> GetMonthOrderAsync(int idStore, int month, int year, bool incluDeleted = false)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn tổng số đơn hàng trong tháng
            int totalOrder = await _dataContext.Orders
                .Where(x => x.Table.IdStore == idStore &&
                            x.CreatedAt >= startDate &&
                            x.CreatedAt <= endDate &&
                            x.IsDeleted == incluDeleted)
                .CountAsync();

            return totalOrder;
        }

        public async Task<int> GetMonthFoodAsync(int idStore, int month, int year, bool incluDeleted = false)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Lấy tất cả các đơn hàng trong tháng
            var monthlyFoods = await _dataContext.Orders
                .Where(x => x.Table.IdStore == idStore &&
                            x.CreatedAt >= startDate &&
                            x.CreatedAt <= endDate &&
                            x.IsDeleted == incluDeleted)
                .Include(x => x.OrderDetails)
                .ToListAsync();

            // Tính tổng số món ăn đã bán trong tháng
            int totalQuantity = monthlyFoods.SelectMany(x => x.OrderDetails).Sum(x => x.Quantity);

            return totalQuantity;
        }

        public async Task<double> GetAVGFoodPerOrderOneMonthAsync(int idStore, int month, int year, bool incluDeleted = false)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var monthlyFood = await _dataContext.Orders.Where(x => x.Table.IdStore == idStore &&
                                                                    x.CreatedAt >= startDate &&
                                                                    x.CreatedAt <= endDate &&
                                                                    x.IsDeleted == incluDeleted)
                                                            .Include(x => x.OrderDetails)
                                                            .ToListAsync();
            int totalFood = monthlyFood.SelectMany(x => x.OrderDetails).Sum(x => x.Quantity);
            var monthlyOrders = await _dataContext.Orders.Where(x => x.Table.IdStore == idStore &&
                                                                    x.CreatedAt >= startDate &&
                                                                    x.CreatedAt <= endDate &&
                                                                    x.IsDeleted == incluDeleted).ToListAsync();
            int totalOrder = monthlyOrders.Count;
            if (totalOrder == 0)
            {

                return 0;
            }
            else
            {
                double AvgFoodPerMonth = totalFood / totalOrder;
                AvgFoodPerMonth = Math.Round(AvgFoodPerMonth, 2);
                return AvgFoodPerMonth;
            }



        }

        public async Task<bool> UpdateHasInvoice(int idOrder, int idInvoice)
        {
            var result = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == idOrder);
            if (result == null)
            {
                throw new Exception("Không tìm thấy");
            }
            result.hasInvoice = true;
            result.IdInvoice = idInvoice;

            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
