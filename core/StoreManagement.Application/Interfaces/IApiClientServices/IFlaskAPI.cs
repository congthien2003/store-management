using StoreManagement.Application.DTOs.Response.OrderDetail;

namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface IFlaskAPI
    {
        Task<List<object>> GetPopularComboAsync(List<DataByIdStoreRes> listData, int idStore);
    }
}
