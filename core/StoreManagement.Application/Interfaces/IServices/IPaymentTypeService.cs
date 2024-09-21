using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IPaymentTypeService
    {
        Task<PaymentTypeDTO> CreateAsync(PaymentTypeDTO paymentTypeDTO);
        Task<PaymentTypeDTO> UpdateAsync(int id, PaymentTypeDTO paymentTypeDTO);
        Task<bool> DeleteAsync(int id);
        Task<PaymentTypeResponse> GetByIdAsync(int id);
        Task<List<PaymentTypeResponse>> GetByNameAsync(int idStore,string name);
        Task<int> GetCountAsync(int idStore, string searchTerm = "");
        Task<List<PaymentTypeResponse>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true);

    }
}
