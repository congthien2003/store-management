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
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, TableDTO tableDTO)
        {

            var result = await _tableService.UpdateAsync(id, tableDTO);
            return Ok(Result<TableDTO?>.Success(result,"Cập nhật thành công"));


        }
        [HttpDelete("delete/{id:int}")]
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
        [HttpGet("all/{idStore:int}")]
        public async Task<ActionResult<Result>> GetByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string sortCol = "", string asc = "true")
        {
            var results = await _tableService.GetAllByIdStore(idStore, currentPage, pageSize, sortCol, asc);
           
            return Ok(Result<PaginationResult<List<TableDTO>>>.Success(results, "Lấy thông tin thành công"));
        }
    }
}
