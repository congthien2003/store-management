using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Request.OrderDetail;
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
        public async Task<List<OrderDetailDTO>> CreateByListAsync(List<OrderDetailDTO> orderDetailDTO)
        {
            List<OrderDetailDTO> listOrderDetailDTO = new List<OrderDetailDTO>();
            foreach (var orderDetail in orderDetailDTO)
            {
                var orderCreated = await _orderDetailRepo.CreateAsync(_mapper.Map<OrderDetail>(orderDetail));
                listOrderDetailDTO.Add(_mapper.Map<OrderDetailDTO>(orderCreated));
            }
            return listOrderDetailDTO;
        }

        public async Task<bool> DeleteAsync(int idOrder, int idFood)
        {
            await _orderDetailRepo.DeleteAsync(idOrder, idFood);
            return true;
        }

        public async Task<PaginationResult<List<OrderDetaiResponse>>> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "10")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);

            List<OrderDetail> list = await _orderDetailRepo.GetAllByIdOrderAsync(idOrder);
            var count = list.Count();
            list = list.Skip(_currentPage * _pageSize - _pageSize).Take(_pageSize).ToList();

            List<OrderDetaiResponse> result = new List<OrderDetaiResponse>();
            foreach (var item in list)
            {
                OrderDetaiResponse temp = new OrderDetaiResponse();
                temp.Food = _mapper.Map<FoodDTO>(item.Food);
                temp.Quantity = item.Quantity;
                temp.Total = temp.Quantity * item.Food.Price;
                temp.statusProcess = item.StatusProcess;
                result.Add(temp);
            }
            return PaginationResult<List<OrderDetaiResponse>>.Create(result, _currentPage, _pageSize, count);
        }

        public async Task<List<OrderDetaiResponse>> GetAllByIdOrderAsync(int idOrder)
        {
            List<OrderDetail> list = await _orderDetailRepo.GetAllByIdOrderAsync(idOrder);

            List<OrderDetaiResponse> result = new List<OrderDetaiResponse>();
            foreach (var item in list)
            {
                OrderDetaiResponse temp = new OrderDetaiResponse();
                temp.Food = _mapper.Map<FoodDTO>(item.Food);
                temp.Quantity = item.Quantity;
                temp.Total = temp.Quantity * item.Food.Price;
                temp.statusProcess = item.StatusProcess;
                result.Add(temp);
            }
            return result;
        }

        public async Task<int> GetCountAsync(int idOrder)
        {
            var count = await _orderDetailRepo.GetCountAsync(idOrder);
            return count;
        }

        public async Task<Dictionary<string, int>> GetFoodSalesReport(int idStore, DateTime startDate, DateTime endDate)
        {
            var orderDetails = await _orderDetailRepo.GetAllOrderDetailsByDay(idStore, startDate, endDate);

            var foodSales = orderDetails
                .Where(od => od.Food != null)
                .GroupBy(od => od.Food.Name)
                .ToDictionary(g => g.Key, g => g.Sum(od => od.Quantity));

            return foodSales;
        }

        public async Task<OrderDetailDTO> UpdateAsync(OrderDetailDTO orderDetailDTO)
        {
            var orderUpdate = _mapper.Map<OrderDetail>(orderDetailDTO);
            var update = await _orderDetailRepo.UpdateAsync(orderUpdate);
            return _mapper.Map<OrderDetailDTO>(update);
        }

        public async Task<OrderDetailDTO> UpdateStatusAsync(UpdateStatusReq req)
        {
            var update = await _orderDetailRepo.UpdateStatusAsync(req.IdOrder, req.IdFood, req.StatusProcess);
            return _mapper.Map<OrderDetailDTO>(update);
        }
    }
}
