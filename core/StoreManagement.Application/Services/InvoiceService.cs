using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
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

        public async Task<PaginationResult<List<InvoiceResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var listInvoice = await _InvoiceRepository.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, sortCol, _asc);

            var responseList = new List<InvoiceResponse>();

            for (int i = 0; i < listInvoice.Count; i++)
            {
                var invoiceResponse = _mapper.Map<InvoiceResponse>(listInvoice[i]);

                if (listInvoice[i].Order != null)
                {
                    invoiceResponse.OrderDTO = new OrderDTO
                    {
                        Id = listInvoice[i].Order.Id,
                        Total = (double)listInvoice[i].Order.Total,
                        NameUser = listInvoice[i].Order.NameUser,
                        PhoneUser = listInvoice[i].Order.PhoneUser,
                        CreatedAt = listInvoice[i].Order.CreatedAt,
                        IdTable = listInvoice[i].Order.IdTable,
                    };
                }
                if (listInvoice[i].PaymentType != null)
                {
                    invoiceResponse.PaymentTypeDTO = new PaymentTypeDTO
                    {
                        Id = listInvoice[i].PaymentType.Id,
                        Name = listInvoice[i].PaymentType.Name,
                        IdStore = listInvoice[i].PaymentType.IdStore,
                    };
                }

                if (listInvoice[i].Voucher != null)
                {
                    invoiceResponse.VoucherDTO = new VoucherDTO
                    {
                        Id = listInvoice[i].Voucher.Id,
                        Name = listInvoice[i].Voucher.Name,
                        Discount = listInvoice[i].Voucher.Discount,
                        IdStore = listInvoice[i].Voucher.IdStore,
                    };
                }

                responseList.Add(invoiceResponse);
            }
            var totalRecords = await _InvoiceRepository.GetCountAsync(idStore);
            return PaginationResult<List<InvoiceResponse>>.Create(responseList,_currentPage,_pageSize, totalRecords);
        }

        public async Task<InvoiceResponse> GetByIdAsync(int id)
        {
            var result = await _InvoiceRepository.GetByIdAsync(id);
            var invoiceResponse = _mapper.Map<InvoiceResponse>(result);

            if (result.Order != null)
            {
                invoiceResponse.OrderDTO = new OrderDTO
                {
                    Id = result.Order.Id,
                    Total = (double)result.Order.Total,
                    NameUser = result.Order.NameUser,
                    PhoneUser = result.Order.PhoneUser,
                    CreatedAt = result.Order.CreatedAt,
                    IdTable = result.Order.IdTable,

                };
            }
            if (result.PaymentType != null)
            {
                invoiceResponse.PaymentTypeDTO = new PaymentTypeDTO
                {
                    Id = result.PaymentType.Id,
                    Name = result.PaymentType.Name,
                    IdStore = result.PaymentType.IdStore,
                };
            }

            // Kiểm tra và gán VoucherDTO nếu không null
            if (result.Voucher != null)
            {
                invoiceResponse.VoucherDTO = new VoucherDTO
                {
                    Id = result.Voucher.Id,
                    Name = result.Voucher.Name,
                    Discount = result.Voucher.Discount,
                    IdStore = result.Voucher.IdStore,
                };
            }

            return invoiceResponse;
        }

        public async Task<int> GetCountAsync(int idStore)
        {
            var count = await _InvoiceRepository.GetCountAsync(idStore);
            return count;
        }

        public async Task<InvoiceDTO> UpdateAsync(int id, InvoiceDTO invoiceDTO)
        {
            var invoiceUpdate = _mapper.Map<Invoice>(invoiceDTO);
            var update = await _InvoiceRepository.UpdateAsync(id, invoiceUpdate);
            return _mapper.Map<InvoiceDTO>(update);
        }
    }
}
