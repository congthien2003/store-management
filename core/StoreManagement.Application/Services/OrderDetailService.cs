using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
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

        public async Task<PaginationResult<List<OrderDetailResponse>>> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "5", string sortCol = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var listDetails = await _orderDetailRepo.GetAllByIdOrderAsync(idOrder, _currentPage, _pageSize, sortCol, _asc);
            var orderDetail = new List<OrderDetailResponse>();
            for (int i = 0; i < listDetails.Count; i++)
            {
                var detailResponse = new OrderDetailResponse
                {
                    Quantity = listDetails[i].Quantity,
                };
                if (listDetails[i].Order != null)
                {
                    detailResponse.OrderDTO = new OrderDTO
                    {
                        Id = listDetails[i].Order.Id,
                        Total = (double)listDetails[i].Order.Total,
                        NameUser = listDetails[i].Order.NameUser,
                        PhoneUser = listDetails[i].Order.PhoneUser,
                        CreatedAt = listDetails[i].Order.CreatedAt,
                        IdTable = listDetails[i].Order.IdTable,
                    };
                }

                // Kiểm tra và gán FoodDTO nếu không null
                if (listDetails[i].Food != null)
                {
                    detailResponse.FoodDTO = new FoodDTO
                    {
                        Id = listDetails[i].Food.Id,
                        Name = listDetails[i].Food.Name,
                        Status = listDetails[i].Food.Status,
                        Quantity = listDetails[i].Food.Quantity,
                        ImageUrl = listDetails[i].Food.ImageUrl,
                        Price = listDetails[i].Food.Price,
                        IdCategory = listDetails[i].Food.IdCategory,
                    };
                }

                orderDetail.Add(detailResponse);
            }
            var totalRecords = await _orderDetailRepo.GetCountAsync(idOrder);
            return PaginationResult<List<OrderDetailResponse>>.Create(orderDetail, _currentPage, _pageSize, totalRecords);
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
