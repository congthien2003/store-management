using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateAsync(FoodDTO foodDTO)
        {
            try
            {
                var food = await _foodService.CreateAsync(foodDTO);
                return Ok(food);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(int id, FoodDTO foodDTO)
        {
            try
            {
                var update = await _foodService.UpdateAsync(id, foodDTO);
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
                var delete = await _foodService.DeleteAsync(id);
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
                var result = await _foodService.GetByIdAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetByNameAsync(int idStore,string name)
        {
            try
            {
                var result = await _foodService.GetByNameAsync(idStore, name);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Store")]
        public async Task<ActionResult> GetAllFoodByIdStore(int idStore,string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true") 
        {
            try
            {
                int _currentPage = int.Parse(currentPage);
                int _pageSize = int.Parse(pageSize);
                bool _asc = bool.Parse(asc);

                var list = await _foodService.GetAllByIdStoreAsync(idStore,_currentPage, _pageSize, searchTerm, sortColumn, _asc, false);
                var count = await _foodService.GetCountList(idStore,searchTerm, false);
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
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Category")]
        public async Task<ActionResult> GetFoodByIdCategory(int id)
        {
            try
            {
                var results = await _foodService.GetByIdCategoryAsync(id);
                return Ok(results);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
