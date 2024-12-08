using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;

namespace StoreManagement.Infrastructure.Repositories
{
    public class OrderAccessTokenRepository : IOrderAccessTokenRepository<OrderAccessToken>
    {
        private readonly DataContext _dataContext;

        public OrderAccessTokenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<OrderAccessToken> Create(OrderAccessToken orderAccess)
        {
            var newToken = await _dataContext.OrderAccessTokens.AddAsync(orderAccess);
            await _dataContext.SaveChangesAsync();
            return newToken.Entity;
        }

        public async Task<OrderAccessToken> GetById(Guid guid)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.Id == guid);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            return token;
        }

        public async Task<OrderAccessToken> GetByURL(string URL)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.QRURL == URL && x.IsPaid == false && x.IsActived == true);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            if (!token.IsPaid)
            {
                token.ExpirationTime = DateTime.UtcNow.AddHours(1);
            }
            return token;
        }

        public async Task<OrderAccessToken> Update(Guid guid, OrderAccessToken orderAccess)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.Id == guid);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            token.QRURL = orderAccess.QRURL;
            token.IdOrder = orderAccess.IdOrder ?? null;
            token.ExpirationTime = orderAccess.ExpirationTime;
            token.IsActived = orderAccess.IsActived;
            _dataContext.OrderAccessTokens.Update(token);
            await _dataContext.SaveChangesAsync();
            return token;
        }

        public async Task<OrderAccessToken> Delete(Guid guid)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.Id == guid);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            _dataContext.Remove(token);
            await _dataContext.SaveChangesAsync();
            return token;
        }

        public async Task<OrderAccessToken> DeleteByIdOrder(int idOrder)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.IdOrder == idOrder);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            _dataContext.Remove(token);
            await _dataContext.SaveChangesAsync();
            return token;
        }

        public async Task<OrderAccessToken> GetByIdOrder(int idOrder)
        {
            var token = await _dataContext.OrderAccessTokens.FirstOrDefaultAsync(x => x.IdOrder == idOrder);
            if (token == null)
            {
                throw new NullReferenceException("Không tìm thấy");
            }
            return token;
        }
    }
}
