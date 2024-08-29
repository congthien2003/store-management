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
        [HttpDelete("delete")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _OrderService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result,"Xóa thành công"));
        }
        [HttpPut("update")]
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
        [HttpGet("all")]
        public async Task<ActionResult> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var list = await _OrderService.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, searchTerm, sortColumn, _asc);
            var count = await _OrderService.GetCountAsync(idStore, searchTerm);
            var _totalPage = count % _pageSize == 0 ? count / _pageSize : count / _pageSize + 1;
            var result = new
            {
                list,
                _currentPage,
                _pageSize,
                _totalPage,
                _totalRecords = count,
                _hasNext = _currentPage < _totalPage,
                _hasPre = _currentPage > 1,
            };
            return Ok(result);
        }
    }
}
