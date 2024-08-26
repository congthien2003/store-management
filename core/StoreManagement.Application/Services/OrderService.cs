using AutoMapper;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IRepositories;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class OrderService : IOrderSerivce
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository<Order> _orderRepository;

        public OrderService(IMapper mapper, IOrderRepository<Order> orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            var orderCreated = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDTO>(orderCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<OrderDTO>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true)
        {
            var listOrders = await _orderRepository.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, sortCol, ascSort);
            return _mapper.Map<List<OrderDTO>>(listOrders);
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<List<OrderDTO>> GetByNameUserAsync(string name)
        {
            var listOrders = await _orderRepository.GetByNameUser(name);
            return _mapper.Map<List<OrderDTO>>(listOrders);
        }

        public async Task<int> GetCountAsync(int idStore, string searchTerm = "")
        {
            var count = await _orderRepository.GetCountAsync(idStore, searchTerm);
            return count;
        }

        public async Task<OrderDTO> UpdateAsync(int id, OrderDTO orderDTO)
        {
            var orderUpdate = _mapper.Map<Order>(orderDTO);
            var update = await _orderRepository.UpdateAsync(id, orderUpdate);
            return _mapper.Map<OrderDTO>(update);
        }
    }
}
