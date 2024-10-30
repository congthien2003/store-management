using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;

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

        /*[HttpGet("idStore:int")]
        public async Task<ActionResult<Result>> GetAllAnalyst(int idStore)
        {
            var result = await _productSellService.GetByIdStoreAsync(idStore);
            return Result<List<ProductSellResponse>>.Success(result, "Lấy thông tin thành công");
        }*/
    }
}
