using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IInvoiceService
    {
        public Task<InvoiceDTO> CreateAsync(InvoiceDTO invoiceDTO);
        public Task<InvoiceDTO> UpdateAsync(int id, InvoiceDTO invoiceDTO);
        public Task<bool> DeleteAsync(int id);
        public Task<InvoiceDTO> GetByIdAsync(int id);
        public Task<PaginationResult<List<InvoiceDTO>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string asc = "true");
        Task<int> GetCountAsync(int idStore);
    }
}
