using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderSerivce _OrderService;

        public OrderController(IOrderSerivce orderSerivce)
        {
            _OrderService = orderSerivce;
        }
        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreateAsync(OrderDTO orderDTO)
        {
            var result = await _OrderService.CreateAsync(orderDTO);
            return Ok(Result<OrderDTO?>.Success(result,"Tạo mới thành công"));

        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _OrderService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result,"Xóa thành công"));
        }
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, OrderDTO orderDTO)
        {
            var result = await _OrderService.UpdateAsync(id, orderDTO);
            return Ok(Result<OrderDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {
            var result = await _OrderService.GetByIdAsync(id);
            return Ok(Result<OrderDTO?>.Success(result, "Lấy thông tin thành công"));
        }
        [HttpGet("all/{idStore:int}")]
        public async Task<ActionResult<Result>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var results = await _OrderService.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, sortColumn, asc);
            
            return Ok(Result<PaginationResult<List<OrderDTO>>>.Success(results, " Lấy thông tin thành công"));
        }
        [HttpGet("caculateTotal/{id:int}")]
        public async Task<ActionResult<Result>> CaculateTotal (int id)
        {
            double total = await _OrderService.CaculateTotal(id);
            return Ok(Result<double?>.Success(total, "Tính tổng thành công"));
        }
    }
}