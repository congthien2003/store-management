using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;

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
        [HttpDelete]
        public async Task<ActionResult<Result>> DeleteAsync(int idFood, int idOrder)
        {
            var result = await _orderDetailService.DeleteAsync(idFood, idOrder);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));
        }
        [HttpGet("IdOrder")]
        public async Task<ActionResult> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "5", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var list = await _orderDetailService.GetAllByIdOrderAsync(idOrder, _currentPage, _pageSize, sortColumn, _asc);
            var count = await _orderDetailService.GetCountAsync(idOrder);
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
