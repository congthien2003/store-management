using AutoMapper;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Services
{
    public class OrderAccessService : IOrderAccessService
    {
        private readonly IOrderAccessTokenRepository<OrderAccessToken> _orderRepository;
        private readonly IMapper _mapper;

        public OrderAccessService(IOrderAccessTokenRepository<OrderAccessToken> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderAccessTokenResponse> Access(OrderAccessTokenDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderAccessTokenResponse> Create(OrderAccessTokenDTO request)
        {
            var create = _mapper.Map<OrderAccessToken>(request);
            var orderaccess = await _orderRepository.Create(create);
            return _mapper.Map<OrderAccessTokenResponse>(orderaccess);
        }

        public async Task<OrderAccessTokenResponse> Delete(Guid id)
        {
            var delete = await _orderRepository.Delete(id);
            return _mapper.Map<OrderAccessTokenResponse>(delete);

        }

        public async Task<OrderAccessTokenResponse> Get(Guid id)
        {
            OrderAccessToken exists = await _orderRepository.GetById(id);
            return _mapper.Map<OrderAccessTokenResponse>(exists);
        }

        public async Task<OrderAccessTokenResponse> GetByURL(string url)
        {
            OrderAccessToken exists = await _orderRepository.GetByURL(url);
            return _mapper.Map<OrderAccessTokenResponse>(exists);
        }

        public async Task<OrderAccessTokenResponse> Request(string URL)
        {
            OrderAccessToken exists = await _orderRepository.GetByURL(URL);
            return _mapper.Map<OrderAccessTokenResponse>(exists);
        }

        public async Task<OrderAccessTokenResponse> Update(Guid id, OrderAccessTokenDTO request)
        {
            var update = _mapper.Map<OrderAccessToken>(request);
            OrderAccessToken exists = await _orderRepository.Update(id, update);
            return _mapper.Map<OrderAccessTokenResponse>(exists);
        }
    }
}
