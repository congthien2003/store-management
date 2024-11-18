using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Domain.Models;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductSellService _productSellService;
        private readonly IOrderSerivce _orderService;
        private readonly ITableService _tableService;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderDetailController(IOrderDetailService orderDetailService,
                                    IProductSellService productSellService,
                                    IHubContext<OrderHub> hubContext,
                                    IOrderSerivce orderService,
                                    ITableService tableService)
        {
            _orderDetailService = orderDetailService;
            _productSellService = productSellService;
            _hubContext = hubContext;
            _orderService = orderService;
            _tableService = tableService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Result>> CreateAsync(List<OrderDetailDTO> orderDetailDTO)
        {
            var result = await _orderDetailService.CreateByListAsync(orderDetailDTO);
            
            foreach (OrderDetailDTO orderDetail in result)
            {
                ProductSellDTO productSellDTO = new ProductSellDTO();
                productSellDTO.FoodId = orderDetail.IdFood;
                productSellDTO.Quantity = orderDetail.Quantity;
                await _productSellService.CreateAsync(productSellDTO);
            }
            
            
            return Ok(Result<List<OrderDetailDTO>>.Success(result,"Tạo mới thành công"));
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(OrderDetailDTO orderDetailDTO)
        {
            var result = await _orderDetailService.UpdateAsync(orderDetailDTO);
            return Ok(Result<OrderDetailDTO?>.Success(result, "Cập nhật thành công"));
        }

        [HttpPut("updateStatus/{idFood:int}")]
        public async Task<ActionResult<Result>> UpdateStatusAsync(int idFood, [FromBody] int statusProcess)
        {
            var result = await _orderDetailService.UpdateStatusAsync(idFood, statusProcess);
            var order = await _orderService.GetByIdAsync(result.IdOrder);
            var table = await _tableService.GetByIdAsync(order.IdTable);
            Console.WriteLine(table.Guid);
            // Send Event to FE
            await _hubContext.Clients.Group(table.Guid).SendAsync("ReceiveUpdateStatusOrder", "Cập nhật trạng thái đơn hàng của bạn");
            return Ok(Result<OrderDetailDTO?>.Success(result, "Cập nhật thành công"));
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int idFood, int idOrder)
        {
            var result = await _orderDetailService.DeleteAsync(idFood, idOrder);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));
        }

        [HttpGet("IdOrder/{idOrder:int}")]
        public async Task<ActionResult> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "10")
        {
            var list = await _orderDetailService.GetAllByIdOrderAsync(idOrder, currentPage, pageSize);
            if (list == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy người dùng"));
            }
            return Ok(Result<PaginationResult<List<OrderDetaiResponse>>>.Success(list, "Lấy thông tin thành công"));
        }

        [HttpGet("IdOrderNoPagi/{idOrder:int}")]
        public async Task<ActionResult> GetAllByIdOrderNoPaiAsync(int idOrder)
        {
            var list = await _orderDetailService.GetAllByIdOrderAsync(idOrder);
            if (list == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy người dùng"));
            }
            return Ok(Result<List<OrderDetaiResponse>>.Success(list, "Lấy thông tin thành công"));
        }
    }
}
