using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentTypeRepository<PaymentType> _paymentTypeRepository;

        public PaymentTypeService(IMapper mapper, IPaymentTypeRepository<PaymentType> paymentTypeRepository)
        {
            _mapper = mapper;
            _paymentTypeRepository = paymentTypeRepository;
        }
        public async Task<PaymentTypeDTO> CreateAsync(PaymentTypeDTO paymentTypeDTO)
        {
           var payment = _mapper.Map<PaymentType>(paymentTypeDTO);
            var paymentCreated = await _paymentTypeRepository.CreateAsync(payment);
            return _mapper.Map<PaymentTypeDTO>(paymentCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _paymentTypeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginationResult<List<PaymentTypeDTO>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var list = await _paymentTypeRepository.GetAllByIdStore(idStore,_currentPage,_pageSize,searchTerm,sortCol,_asc);
            var count = list.Count();
            var listPayment = _mapper.Map<List<PaymentTypeDTO>>(list);
            return PaginationResult<List<PaymentTypeDTO>>.Create(listPayment, _currentPage, _pageSize, count);
        }

        public async Task<PaymentTypeDTO> GetByIdAsync(int id)
        {
            var payment = await _paymentTypeRepository.GetByIdAsync(id);
            return _mapper.Map<PaymentTypeDTO>(payment);
        }

        public async Task<List<PaymentTypeDTO>> GetByNameAsync(int idStore, string name)
        {
            var listPayments = await _paymentTypeRepository.GetByNameAsync(idStore,name);
            return _mapper.Map<List<PaymentTypeDTO>>(listPayments);
        }

        public async Task<int> GetCountAsync(int idStore, string searchTerm = "")
        {
            var count = await _paymentTypeRepository.GetCountAsync(idStore, searchTerm);
            return count;
        }

        public async Task<PaymentTypeDTO> UpdateAsync(int id, PaymentTypeDTO paymentTypeDTO)
        {
            var paymentUpdate = _mapper.Map<PaymentType>(paymentTypeDTO);
            var update = await _paymentTypeRepository.UpdateAsync(id, paymentUpdate);
            return _mapper.Map<PaymentTypeDTO>(update);
        }
    }
}
