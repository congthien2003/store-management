﻿using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class OrderService : IOrderSerivce
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly ITableRepository<Table> _tableRepository;

        public OrderService(IMapper mapper, IOrderRepository<Order> orderRepository, ITableRepository<Table> tableRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
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

        public async Task<PaginationResult<List<OrderResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var list = await _orderRepository.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, searchTerm, sortCol, _asc);
            var listOrders = new List<OrderResponse>();
            foreach (var order in list)
            {
                var orderResponse = _mapper.Map<OrderResponse>(order);
                var table = await _tableRepository.GetByIdAsync(order.IdTable);
                if (table != null)
                {
                    orderResponse.TableDTO = _mapper.Map<TableDTO>(table);
                }
                listOrders.Add(orderResponse);
            }
            var totalRecords = await _orderRepository.GetCountAsync(idStore);
            return PaginationResult<List<OrderResponse>>.Create(listOrders,_currentPage,_pageSize,totalRecords);
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            if (order.Table != null)
            {
                orderResponse.TableDTO = new TableDTO
                {
                    Id = order.Table.Id,
                    Status = order.Table.Status,
                    IdStore = order.Table.IdStore,
                };

            }
            return orderResponse;
        }

        public async Task<List<OrderResponse>> GetByNameUserAsync(string name)
        {
            var listOrders = await _orderRepository.GetByNameUser(name);
            return _mapper.Map<List<OrderResponse>>(listOrders);
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
        public async Task<double> CaculateTotal(int id, bool incluDeleted = false)
        {
            var total = await _orderRepository.CaculateTotal(id);
            return total;
        }
    }
}
