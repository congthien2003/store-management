using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Application.Services;
using StoreManagement.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSellController : ControllerBase
    {
        private readonly IProductSellService productSellService;

        public ProductSellController(IProductSellService productSellService)
        {
            this.productSellService = productSellService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<Result>> GetAllStore(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var productsell = await productSellService.GetAllAsync(currentPage, pageSize, searchTerm, sortColumn, asc);
            if (productsell == null)
            {
                return BadRequest(Result.Failure("không tìm thấy cửa hàng"));
            }
            return Ok(Result<PaginationResult<List<ProductSellResponse>>>.Success(productsell, "lấy thông tin thành công"));
        }
    }
}
