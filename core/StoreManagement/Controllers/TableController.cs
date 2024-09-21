using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;

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
        public async Task<ActionResult<Result>> CreateAsync(TableDTO tableDTO)
        {
            var result = await _tableService.CreateAsync(tableDTO);
            return Ok(Result<TableDTO?>.Success(result,"Tạo mới thành công"));
        }
        [HttpPut("update")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, TableDTO tableDTO)
        {

            var result = await _tableService.UpdateAsync(id, tableDTO);
            return Ok(Result<TableDTO?>.Success(result,"Cập nhật thành công"));


        }
        [HttpDelete("delete")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {

            var result = await _tableService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));


        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {

            var result = await _tableService.GetByIdAsync(id);
            return Ok(Result<TableResponse?>.Success(result, "Lấy thông tin thành công"));


        }
        [HttpGet("all")]
        public async Task<ActionResult> GetByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string sortCol = "", string ascSort = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(ascSort);

            var list = await _tableService.GetAllByIdStore(idStore, _currentPage, _pageSize, sortCol, _asc);
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
    }
}
