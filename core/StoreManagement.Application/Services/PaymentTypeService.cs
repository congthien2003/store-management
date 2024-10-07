using AutoMapper;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
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

        public async Task<List<PaymentTypeResponse>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true)
        {
            var listPayment = await _paymentTypeRepository.GetAllByIdStore(idStore,currentPage,pageSize,searchTerm,sortColumn,ascSort);
            var responseList = new List<PaymentTypeResponse>();

            for (int i = 0; i < listPayment.Count; i++)
            {
                var paymentResponse = _mapper.Map<PaymentTypeResponse>(listPayment[i]);

                if (listPayment[i].Store != null)
                {
                    paymentResponse.StoreDTO = new StoreDTO
                    {
                        Id = listPayment[i].Store.Id,
                        Name = listPayment[i].Store.Name,
                        Address = listPayment[i].Store.Address,
                        Phone = listPayment[i].Store.Phone,
                        IdUser = listPayment[i].Store.IdUser
                    };
                }

                responseList.Add(paymentResponse);
            }

            return responseList;
        }

        public async Task<PaymentTypeResponse> GetByIdAsync(int id)
        {
            var payment = await _paymentTypeRepository.GetByIdAsync(id);
            var paymentResponse = _mapper.Map<PaymentTypeResponse>(payment);

            if (payment.Store != null)
            {
                paymentResponse.StoreDTO = new StoreDTO
                {
                    Id = payment.Store.Id,
                    Name = payment.Store.Name,
                    Address = payment.Store.Address,
                    Phone = payment.Store.Phone,
                    IdUser = payment.Store.IdUser
                };
            }

            return paymentResponse;
        }

        public async Task<List<PaymentTypeResponse>> GetByNameAsync(int idStore, string name)
        {
            var listPayments = await _paymentTypeRepository.GetByNameAsync(idStore,name);
            var responseList = new List<PaymentTypeResponse>();

            for (int i = 0; i < listPayments.Count; i++)
            {
                var paymentResponse = _mapper.Map<PaymentTypeResponse>(listPayments[i]);
                if (listPayments[i].Store != null)
                {
                    paymentResponse.StoreDTO = new StoreDTO
                    {
                        Id = listPayments[i].Store.Id,
                        Name = listPayments[i].Store.Name,
                        Address = listPayments[i].Store.Address,
                        Phone = listPayments[i].Store.Phone,
                        IdUser = listPayments[i].Store.IdUser
                    };
                }

                responseList.Add(paymentResponse);
            }

            return responseList;
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
