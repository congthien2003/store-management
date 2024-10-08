using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
        public interface IPaymentTypeService
        {
                Task<PaymentTypeDTO> CreateAsync(PaymentTypeDTO paymentTypeDTO);
                Task<PaymentTypeDTO> UpdateAsync(int id, PaymentTypeDTO paymentTypeDTO);
                Task<bool> DeleteAsync(int id);
                Task<PaymentTypeResponse> GetByIdAsync(int id);
                Task<List<PaymentTypeResponse>> GetByNameAsync(int idStore, string name);
                Task<int> GetCountAsync(int idStore, string searchTerm = "");
                Task<PaginationResult<List<PaymentTypeResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string ascSort = "true");

        }
}
