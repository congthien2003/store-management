using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IOrderDetailService
    {
        Task<OrderDetailDTO> CreateAsync(OrderDetailDTO orderDetailDTO);
        Task<OrderDetailDTO> UpdateAsync(OrderDetailDTO orderDetailDTO);
        Task<bool> DeleteAsync(int idOrder, int idFood);
        Task<int> GetCountAsync(int idOrder);
        Task<List<OrderDetailResponse>> GetAllByIdOrderAsync(int idOrder, int currentPage = 1, int pageSize = 5, string sortCol = "", bool ascSort = true);
    }
}
