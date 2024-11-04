﻿using StoreManagement.Infrastructure.Data;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.WebSockets;
using StoreManagement.Domain.IRepositories;

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

            var item = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdFood == orderDetail.IdFood);
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
            if(orderDetail == null)
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
            if(orderUpdate == null)
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

        public async Task<OrderDetail> UpdateStatusAsync(int idFood, int statusProcess)
        {
            var exists = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.IdFood == idFood);
            if(exists == null)
            {
                throw new KeyNotFoundException("Không tìm thấy chi tiết ");

            }
            exists.StatusProcess = statusProcess;
            await _dataContext.SaveChangesAsync();
            return exists;
        }
    }
}
