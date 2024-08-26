using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService) 
        {
            _tableService = tableService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(TableDTO tableDTO)
        {
            try
            {
                var table = await _tableService.CreateAsync(tableDTO);
                return Ok(table);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(int id, TableDTO tableDTO)
        {
            try
            {
                var update = await _tableService.UpdateAsync(id, tableDTO);
                return Ok(update);
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
                var delete = await _tableService.DeleteAsync(id);
                return Ok(delete);
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
                var result = await _tableService.GetByIdAsync(id);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string sortCol = "", string ascSort = "true")
        {
            try
            {
                int _currentPage = int.Parse(currentPage);
                int _pageSize = int.Parse(pageSize);
                bool _asc = bool.Parse(ascSort);

                var list = await _tableService.GetAllByIdStore(idStore,_currentPage, _pageSize, sortCol, _asc);
                var count = await _tableService.GetCountAsync(idStore);
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
