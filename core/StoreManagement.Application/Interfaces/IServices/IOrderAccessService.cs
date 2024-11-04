using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;


namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IOrderAccessService
    {
        Task<OrderAccessTokenResponse> Access(OrderAccessTokenDTO request);
        Task<OrderAccessTokenResponse> Create(OrderAccessTokenDTO request);
        Task<OrderAccessTokenResponse> Update(Guid id, OrderAccessTokenDTO request);
        Task<OrderAccessTokenResponse> Delete(Guid id);
        Task<OrderAccessTokenResponse> Get(Guid id);
        Task<OrderAccessTokenResponse> GetByURL(string URL);
        Task<OrderAccessTokenResponse> Request(string URL);



    }
}
