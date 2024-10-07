using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using System.Collections.Generic;

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
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            var result = await _categoryService.UpdateAsync(id, categoryDTO);
            return Ok(Result<CategoryDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("delete/{id}")]
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
        [HttpGet("store/{idStore}")]
        public async Task<ActionResult<Result>> GetAllCategoryByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var results = await _categoryService.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, sortColumn, asc);
            return Ok(Result<PaginationResult<List<CategoryDTO>>>.Success(results,"Lấy thông tin thành công"));
        }
    }
}
