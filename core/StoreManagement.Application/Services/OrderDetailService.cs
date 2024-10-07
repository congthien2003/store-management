using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository<OrderDetail> _orderDetailRepo;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository<OrderDetail> orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepo = orderDetailRepository;
            _mapper = mapper;
        }
        public async Task<OrderDetailDTO> CreateAsync(OrderDetailDTO orderDetailDTO)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDTO);
            var orderCreated = await _orderDetailRepo.CreateAsync(orderDetail);
            return _mapper.Map<OrderDetailDTO>(orderCreated);
        }

        public async Task<bool> DeleteAsync(int idOrder, int idFood)
        {
            await _orderDetailRepo.DeleteAsync(idOrder, idFood);
            return true;
        }

        public async Task<PaginationResult<List<OrderDetailDTO>>> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "5", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var list = await _orderDetailRepo.GetAllByIdOrderAsync(idOrder, _currentPage, _pageSize, sortCol, _asc);
            var count = list.Count();
            var listOrderDetail = _mapper.Map<List<OrderDetailDTO>>(list);
            return PaginationResult<List<OrderDetailDTO>>.Create(listOrderDetail, _currentPage, _pageSize, count);
        }

        public async Task<int> GetCountAsync(int idOrder)
        {
            var count = await _orderDetailRepo.GetCountAsync(idOrder);
            return count;
        }

        public async Task<OrderDetailDTO> UpdateAsync(OrderDetailDTO orderDetailDTO)
        {
            var orderUpdate = _mapper.Map<OrderDetail>(orderDetailDTO);
            var update = await _orderDetailRepo.UpdateAsync(orderUpdate);
            return _mapper.Map<OrderDetailDTO>(update);
        }
    }
}
