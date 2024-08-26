using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateAsync(StoreDTO storeDTO)
        {
            try
            {
                var store = await storeService.CreateAsync(storeDTO);
                return Ok(store);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllStore(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var list = await storeService.GetAllAsync(_currentPage, _pageSize, searchTerm, sortColumn, _asc);
            var count = await storeService.GetCountList(searchTerm);
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
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetStoreById(int id)
        {
            try
            {
                var result = await storeService.GetByIdAsync(id);  
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult> GetStoreByName(string name)
        {
            try
            {
                var result = await storeService.GetByNameAsync(name);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateStore(int id, StoreDTO storeDTO)
        {
            try
            {
                var update = await storeService.UpdateAsync(id, storeDTO);
                return Ok(update);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteStore(int id)
        {
            try
            {
                var delete = await storeService.DeleteAsync(id);
                return Ok(delete);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
