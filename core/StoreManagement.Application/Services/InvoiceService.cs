using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository<Invoice> _InvoiceRepository;

        public InvoiceService(IInvoiceRepository<Invoice> invoiceRepository, IMapper mapper) 
        {
            _mapper = mapper;
            _InvoiceRepository = invoiceRepository;
        }
        public async Task<InvoiceDTO> CreateAsync(InvoiceDTO invoiceDTO)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDTO);
            var created = await _InvoiceRepository.CreateAsync(invoice);
            return _mapper.Map<InvoiceDTO>(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _InvoiceRepository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginationResult<List<InvoiceDTO>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var totalRecord = await _InvoiceRepository.GetCountAsync(idStore);
            var list = await _InvoiceRepository.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize,sortCol,_asc);
            var count = list.Count();
            var listInvoice = _mapper.Map<List<InvoiceDTO>>(list);
            return PaginationResult<List<InvoiceDTO>>.Create(listInvoice, _currentPage, _pageSize, totalRecord);
        }

        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var result = await _InvoiceRepository.GetByIdAsync(id);
            return _mapper.Map<InvoiceDTO>(result);
        }

        public async Task<int> GetCountAsync(int idStore)
        {
            var count = await _InvoiceRepository.GetCountAsync(idStore);
            return count;
        }

        public async Task<InvoiceDTO> UpdateAsync(int id, InvoiceDTO invoiceDTO)
        {
            var invoiceUpdate = _mapper.Map<Invoice>(invoiceDTO);
            var update = await _InvoiceRepository.UpdateAsync(id,invoiceUpdate);
            return _mapper.Map<InvoiceDTO>(update);
        }
    }
}
