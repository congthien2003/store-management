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
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Result>> CreateAsync(FoodDTO foodDTO)
        {
            var result = await _foodService.CreateAsync(foodDTO);
            return Ok(Result<FoodDTO?>.Success(result, "Tạo mới thành công"));
        }
        [HttpPut("update")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, FoodDTO foodDTO)
        {
            var result = await _foodService.UpdateAsync(id, foodDTO);
            return Ok(Result<FoodDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _foodService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result,"Xóa thành công"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {

            var result = await _foodService.GetByIdAsync(id);
            return Ok(Result<FoodResponse?>.Success(result, "Lấy thông tin thành công"));
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetByNameAsync(int idStore, string name)
        {
            var result = await _foodService.GetByNameAsync(idStore, name);
            return Ok(result);
        }
        [HttpGet("Store")]
        public async Task<ActionResult> GetAllFoodByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var list = await _foodService.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, searchTerm, sortColumn, _asc, false);
            var count = await _foodService.GetCountList(idStore, searchTerm, false);
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
        [HttpGet("Category")]
        public async Task<ActionResult> GetFoodByIdCategory(int id)
        {
            var results = await _foodService.GetByIdCategoryAsync(id);
            return Ok(results);
        }
    }
}
