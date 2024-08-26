using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateAsync(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var orderDetail = await _orderDetailService.CreateAsync(orderDetailDTO);
                return Ok(orderDetail);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var update = await _orderDetailService.UpdateAsync(orderDetailDTO);
                return Ok(update);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int idFood, int idOrder)
        {
            try
            {
                var delete = await _orderDetailService.DeleteAsync(idFood, idOrder);
                return Ok(delete);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("IdOrder")]
        public async Task<ActionResult> GetAllByIdOrderAsync(int idOrder, string currentPage = "1", string pageSize = "5", string sortColumn = "", string asc = "true")
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
