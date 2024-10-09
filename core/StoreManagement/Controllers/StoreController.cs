﻿using StoreManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StoreManagement.Application.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            var result = await storeService.CreateAsync(storeDTO);
            return Ok(Result<StoreDTO?>.Success(result, "Tạo cửa hàng thành công"));
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<Result>> GetAllStore(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var store = await storeService.GetAllAsync(currentPage, pageSize, searchTerm, sortColumn, asc);
            if (store == null)
            {
                return BadRequest(Result.Failure("không tìm thấy cửa hàng"));
            }
            return Ok(Result<PaginationResult<List<StoreResponse>>>.Success(store, "lấy thông tin thành công"));
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Result>> GetStoreById(int id)
        {
            var result = await storeService.GetByIdAsync(id);
            return Ok(Result<StoreResponse?>.Success(result, "Lấy thông tin cửa hàng thành công"));
        }
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult> GetStoreByName(string name)
        {
            var result = await storeService.GetByNameAsync(name);
            return Ok(result);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Result>> UpdateStore(int id, StoreDTO storeDTO)
        {
            var result = await storeService.UpdateAsync(storeDTO);
            return Ok(Result<StoreDTO?>.Success(result, "Cập nhật thành công"));
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Result>> DeleteStore(int id)
        {
            var result = await storeService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result, "Cập nhật thành công"));
        }
        [HttpGet("User/{idUser}")]
        public async Task<ActionResult<Result>> GetByIdUser(int idUser)
        {
            var result = await storeService.GetByIdUserAsync(idUser);
            return Ok(Result<StoreResponse?>.Success(result, "Lấy thông tin thành công"));
        }
    }
}
