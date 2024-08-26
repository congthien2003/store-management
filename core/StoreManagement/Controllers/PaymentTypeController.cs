
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService) 
        {
            _paymentTypeService = paymentTypeService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(PaymentTypeDTO paymentTypeDTO)
        {
            try
            {
                var result = await _paymentTypeService.CreateAsync(paymentTypeDTO);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(int id, PaymentTypeDTO paymentTypeDTO)
        {
            try
            {
                var result = await _paymentTypeService.UpdateAsync(id, paymentTypeDTO);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _paymentTypeService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _paymentTypeService.GetByIdAsync(id);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetByNameAsync(int idStore, string name)
        {
            try
            {
                var results = await _paymentTypeService.GetByNameAsync(idStore, name);
                return Ok(results);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            try
            {
                int _currentPage = int.Parse(currentPage);
                int _pageSize = int.Parse(pageSize);
                bool _asc = bool.Parse(asc);

                var list = await _paymentTypeService.GetAllByIdStoreAsync(idStore,_currentPage, _pageSize, searchTerm, sortColumn, _asc);
                var count = await _paymentTypeService.GetCountAsync(idStore,searchTerm);
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
