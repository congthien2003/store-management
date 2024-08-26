using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService) 
        {
            _invoiceService = invoiceService; 
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(InvoiceDTO invoiceDTO)
        {
            try
            {
                var invoice = await _invoiceService.CreateAsync(invoiceDTO);
                return Ok(invoice);
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
                var delete = await _invoiceService.DeleteAsync(id);
                return Ok(delete);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(int id, InvoiceDTO invoiceDTO)
        {
            try
            {
                var update = await _invoiceService.UpdateAsync(id, invoiceDTO);
                return Ok(update);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _invoiceService.GetByIdAsync(id);
                return Ok(result);
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

                var list = await _invoiceService.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize,searchTerm ,sortColumn, _asc);
                var count = await _invoiceService.GetCountAsync(idStore);
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
