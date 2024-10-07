using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Application.Interfaces.IServices
{
        public interface IInvoiceService
        {
                public Task<InvoiceDTO> CreateAsync(InvoiceDTO invoiceDTO);
                public Task<InvoiceDTO> UpdateAsync(int id, InvoiceDTO invoiceDTO);
                public Task<bool> DeleteAsync(int id);
                public Task<InvoiceResponse> GetByIdAsync(int id);
                public Task<List<InvoiceResponse>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true);
                Task<int> GetCountAsync(int idStore);
        }
}
