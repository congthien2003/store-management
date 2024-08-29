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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Result>> CreateAsync(CategoryDTO categoryDTO)
        {
            var result = await _categoryService.CreateAsync(categoryDTO);
            return Ok(Result<CategoryDTO?>.Success(result, "Tạo mới thành công"));
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            var result = await _categoryService.UpdateAsync(id, categoryDTO);
            return Ok(Result<CategoryDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result,"Đã xóa thành công"));
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Result>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(Result<CategoryDTO?>.Success(result, "Lấy thông tin thành công"));
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetCategoryByName(int idStore, string name)
        {
            var result = await _categoryService.GetByNameAsync(idStore, name);
            return Ok(result);
        }
        [HttpGet("store")]
        public async Task<ActionResult> GetAllCategoryByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var list = await _categoryService.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, searchTerm, sortColumn, _asc, false);
            var count = await _categoryService.GetCountList(idStore, searchTerm, false);
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
