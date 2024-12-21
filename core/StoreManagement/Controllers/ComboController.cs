using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request.Combo;
using StoreManagement.Application.DTOs.Response.Combo;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService _comboService;

        public ComboController(IComboService comboService)
        {
            _comboService = comboService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Result>> CreateAsync(CreateComboReq combo)
        {
            var result = await _comboService.CreateAsync(combo);
            return Ok(Result<ComboDTO?>.Success(result, "Tạo mới thành công"));
        }
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, ComboDTO ComboDTO)
        {
            var result = await _comboService.UpdateAsync(id, ComboDTO);
            return Ok(Result<ComboDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _comboService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {

            var result = await _comboService.GetByIdAsync(id);
            return Ok(Result<ComboDTO?>.Success(result, "Lấy thông tin thành công"));
        }

        [HttpGet("Store")]
        public async Task<ActionResult> GetAllComboByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string filter = "false", int? categoryId = null)
        {

            var list = await _comboService.GetAllAsync(idStore, currentPage, pageSize, searchTerm, filter);
            if (list == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy"));
            }
            return Ok(Result<PaginationResult<List<ComboWithFood>>>.Success(list, "Lấy thông tin thành công"));
        }

    }
}
