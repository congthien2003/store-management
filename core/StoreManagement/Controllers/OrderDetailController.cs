using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Result>> CreateAsync(OrderDetailDTO orderDetailDTO)
        {
            var result = await _orderDetailService.CreateAsync(orderDetailDTO);
            return Ok(Result<OrderDetailDTO?>.Success(result,"Tạo mới thành công"));
        }
        [HttpPut("update")]
        public async Task<ActionResult<Result>> UpdateAsync(OrderDetailDTO orderDetailDTO)
        {
            var result = await _orderDetailService.UpdateAsync(orderDetailDTO);
            return Ok(Result<OrderDetailDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("delete/{idFood:int}/{idOrder:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int idFood, int idOrder)
        {
            var result = await _orderDetailService.DeleteAsync(idFood, idOrder);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));
        }
        [HttpGet("idOrder/{idOrder:int}")]
        public async Task<ActionResult<Result>> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "5", string sortColumn = "", string asc = "true")
        {
            var result = await _orderDetailService.GetAllByIdOrderAsync(idOrder, currentPage, pageSize, sortColumn, asc);
            
            return Ok(Result<PaginationResult<List<OrderDetailResponse>>>.Success(result, "Lấy thông tin thành công"));
        }
    }
}
