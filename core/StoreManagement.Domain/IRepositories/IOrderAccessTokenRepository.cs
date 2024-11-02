using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
