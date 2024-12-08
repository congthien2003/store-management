using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IOrderAccessTokenRepository<TOrderAccessToken> where TOrderAccessToken : OrderAccessToken
    {
        Task<TOrderAccessToken> GetById(Guid guid);
        Task<TOrderAccessToken> GetByURL(string URL);

        Task<TOrderAccessToken> Create(OrderAccessToken orderAccess);
        Task<TOrderAccessToken> Update(Guid guid, OrderAccessToken orderAccess);
        Task<TOrderAccessToken> Delete(Guid guid);
        Task<TOrderAccessToken> DeleteByIdOrder(int idOrder);

        Task<TOrderAccessToken> GetByIdOrder(int idOrder);

    }
}
