﻿using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services
{
    public class ProductSellService : IProductSellService
    {
        private readonly IMapper _mapper;
        private readonly IProductSellRepository<ProductSell> _productSellRepository;
        private readonly IOrderDetailRepository<OrderDetail> _orderDetailRepository;
        private readonly IFoodRepository<Food> _foodRepository;

        public ProductSellService(IMapper mapper, IProductSellRepository<ProductSell> productSellRepository, IOrderDetailRepository<OrderDetail> orderDetailRepository, IFoodRepository<Food> foodRepository)
        {
            _mapper = mapper;
            _productSellRepository = productSellRepository;
            _orderDetailRepository = orderDetailRepository;
            _foodRepository = foodRepository;
        }
        public async Task<DTOs.Request.ProductSellDTO> CreateAsync(DTOs.Request.ProductSellDTO productSellDTO)
        {
            var productSell = _mapper.Map<ProductSell>(productSellDTO);
            var created = await _productSellRepository.CreateAsync(productSell);
            return _mapper.Map<DTOs.Request.ProductSellDTO>(created);    
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _productSellRepository.DeleteAsync(id);   
            return true;
        }

        public async Task<PaginationResult<List<ProductSellResponse>>> GetAllAsync(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var totalRecords = await _productSellRepository.CountAsync(searchTerm);
            var list = await _productSellRepository.GetAll(_currentPage, _pageSize, searchTerm, sortColumn, _asc);

            var listProduct = new List<ProductSellResponse>();
            foreach (var product in list)
            {
                var productResponse = _mapper.Map<ProductSellResponse>(product);
                var food = await _foodRepository.GetByIdAsync(product.FoodId);

                if (food != null)
                {
                    productResponse.FoodDTO = _mapper.Map<FoodDTO>(food);
                }

                listProduct.Add(productResponse);
            }

            return PaginationResult<List<DTOs.Response.ProductSellResponse>>.Create(listProduct, _currentPage, _pageSize, totalRecords);
        }

        public async Task<DTOs.Request.ProductSellDTO> GetByIdAsync(int id)
        {
            var productSell = await _productSellRepository.GetByIdAsync(id);
            return _mapper.Map<DTOs.Request.ProductSellDTO>(productSell);
        }

        public async Task<DTOs.Request.ProductSellDTO> UpdateAsync(int id, DTOs.Request.ProductSellDTO productSellDTO)
        {
            var update = _mapper.Map<ProductSell>(productSellDTO);
            var updated = await _productSellRepository.UpdateAsync(id, update);
            return _mapper.Map<DTOs.Request.ProductSellDTO>(updated);
        }
    }
}
