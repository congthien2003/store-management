using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result>> CraeteAsync(StaffDTO staffDTO)
        {
            var result = await _staffService.CreateAsync(staffDTO);
            return Ok(Result<StaffDTO>.Success(result, "Tạo nhân viên thành công"));
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _staffService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result, "Gửi thư thành công"));
        }
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, StaffDTO staffDTO)
        {
            var result = await _staffService.UpdateAsync(id, staffDTO);
            return Ok(Result<StaffDTO>.Success(result,"Cập nhật thành công"));
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {
            var result = await _staffService.GetByIdAsync(id);
            return Ok(Result<StaffResponse>.Success(result,"Lấy thông tin thành côgn"));
        }
        [HttpGet("all/{idStore:int}")]
        public async Task<ActionResult<Result>> GetAllAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string filter = "false")
        {
            var list = await _staffService.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, filter);
            if (list == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy nhân viên"));
            }
            return Ok(Result<PaginationResult<List<StaffResponse>>>.Success(list,"Lấy thông tin thành công"));
        }
        [HttpGet("name/{idStore:int}")]
        public async Task<ActionResult<Result>> GetByNameAsync(string name, int idStore)
        {
            var list = await _staffService.GetByNameAsync(name, idStore);
            if (list == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy nhân viên"));
            }
            return Ok(Result<List<StaffResponse>>.Success(list, "Lấy thông tin thành công"));
        }
    }
}
