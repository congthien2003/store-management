using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Application.Services;
using StoreManagement.Domain.Models;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSellController : ControllerBase
    {
        private readonly IProductSellService _productSellService;

        public ProductSellController(IProductSellService productSellService)
        {
            _productSellService = productSellService;
        }

        //[HttpGet("idStore:int")]
        //public async Task<ActionResult<Result>> GetAllAnalyst(int idStore)
        //{
        //    var result = await _productSellService.GetByIdStoreAsync(idStore);
        //    return Result<List<ProductSellResponse>>.Success(result, "Lấy thông tin thành công");
        //}
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<Result>> GetAllStore(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var productsell = await _productSellService.GetAllAsync(currentPage, pageSize, searchTerm, sortColumn, asc);
            if (productsell == null)
            {
                return BadRequest(Result.Failure("không tìm thấy cửa hàng"));
            }
            return Ok(Result<PaginationResult<List<ProductSellResponse>>>.Success(productsell, "lấy thông tin thành công"));
        }
    }
}
