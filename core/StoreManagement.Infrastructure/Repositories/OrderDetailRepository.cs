﻿using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreManagement.Infrastructure.Repositories
{

    public class OrderDetailRepository : IOrderDetailRepository<OrderDetail>
    {
        private readonly DataContext _dataContext;

        public OrderDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<OrderDetail> CreateAsync(OrderDetail orderDetail)
        {
            var existOrder = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == orderDetail.IdOrder && x.IsDeleted == false);
            if (existOrder == null)
            {
                throw new InvalidOperationException("Order không tồn tại");
            }
            var food = await _dataContext.Foods.FirstOrDefaultAsync(x => x.Id == orderDetail.IdFood && x.IsDeleted == false);
            if (food == null)
            {
                throw new InvalidOperationException("Đồ ăn không tồn tại");
            }

            var item = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdFood == orderDetail.IdFood && x.IdOrder == orderDetail.IdOrder);
            if (item == null)
            {
                var create = await _dataContext.OrderDetails.AddAsync(orderDetail);
                await _dataContext.SaveChangesAsync();
                return create.Entity;
            }
            else
            {
                item.Quantity += orderDetail.Quantity;
                item.StatusProcess = 1;
                await _dataContext.SaveChangesAsync();
                return item;
            }

        }

        public async Task<OrderDetail> DeleteAsync(int idOrder, int idFood)
        {
            var orderDetail = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdFood == idFood && x.IdOrder == idOrder);
            if (orderDetail == null)
            {
                throw new KeyNotFoundException("Không tìm thấy chi tiết order");
            }
            _dataContext.OrderDetails.Remove(orderDetail);
            await _dataContext.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<List<OrderDetail>> GetAllByIdOrderAsync(int idOrder)
        {
            var exists = await _dataContext.OrderDetails.Where(x => x.IdOrder == idOrder).Include("Food").ToListAsync();
            if (exists.Count == 0)
            {
                throw new KeyNotFoundException("Không tìm thấy");
            }
            return exists;
        }
        public Expression<Func<OrderDetail, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                default:
                    return x => x.IdOrder;
            }
        }
        public async Task<int> GetCountAsync(int idOrder)
        {
            var orderDetail = _dataContext.OrderDetails.Where(x => x.IdOrder == idOrder).AsQueryable();
            await orderDetail.ToListAsync();
            return orderDetail.Count();
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
        {
            var orderUpdate = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdOrder == orderDetail.IdOrder && x.IdFood == orderDetail.IdFood);
            if (orderUpdate == null)
            {
                throw new KeyNotFoundException("Không tìm thấy chi tiết ");
            }
            orderUpdate.Quantity = orderDetail.Quantity;
            orderUpdate.StatusProcess = 1;
            _dataContext.OrderDetails.Update(orderUpdate);
            return orderUpdate;
        }

        public async Task<OrderDetail> GetByID(int idFood)
        {
            var exists = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdFood == idFood);
            return null;
        }

        public async Task<OrderDetail> UpdateStatusAsync(int idOrder, int idFood, int statusProcess)
        {
            var exists = await _dataContext.OrderDetails.Include("Order").FirstOrDefaultAsync(x => x.IdFood == idFood && x.IdOrder == idOrder);
            if (exists == null)
            {
                throw new KeyNotFoundException("Không tìm thấy");

            }
            exists.StatusProcess = statusProcess;
            await _dataContext.SaveChangesAsync();
            return exists;
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailsByDay(int idStore, DateTime startDate, DateTime endDate)
        {
            endDate = endDate.Date.AddDays(1);
            var listOrderDetail = await _dataContext.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(o => o.Table)
                .Include(od => od.Food)
                .Where(od => od.Order.Table.IdStore == idStore && od.Order.CreatedAt >= startDate && od.Order.CreatedAt < endDate)
                .ToListAsync();

            return listOrderDetail;
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailsByIdStore(int idStore)
        {
            var listOrderDetail = await _dataContext.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(o => o.Table)
                .Include(od => od.Food)
                .Where(od => od.Order.Table.IdStore == idStore)
                .ToListAsync();

            return listOrderDetail;
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(int idOrder)
        {
            return await _dataContext.OrderDetails
                .Where(od => od.IdOrder == idOrder)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetByOrderIdAndFoodIdAsync(int orderId, int foodId)
        {
            return await _dataContext.OrderDetails
                .FirstOrDefaultAsync(od => od.IdOrder == orderId && od.IdFood == foodId);
        }

        public async Task<bool> ExistsAsync(int orderId, int foodId)
        {
            return await _dataContext.OrderDetails
                .AnyAsync(od => od.IdOrder == orderId && od.IdFood == foodId);
        }

        public async Task<bool> UpdateStatusDone(int idOrder)
        {
            // Lấy danh sách các OrderDetails thuộc idOrder
            var orderDetails = _dataContext.OrderDetails.Where(od => od.IdOrder == idOrder);

            if (!orderDetails.Any())
            {
                // Nếu không có item nào, trả về false
                return false;
            }

            // Cập nhật statusProgress = 2 cho tất cả các mục
            foreach (var item in orderDetails)
            {
                item.StatusProcess = 2;
            }

            // Lưu thay đổi vào database
            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}
