
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }
        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreateAsync(PaymentTypeDTO paymentTypeDTO)
        {
            var result = await _paymentTypeService.CreateAsync(paymentTypeDTO);
            return Ok(Result<PaymentTypeDTO?>.Success("Tạo mới thành công"));
        }
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id, PaymentTypeDTO paymentTypeDTO)
        {
            var result = await _paymentTypeService.UpdateAsync(id, paymentTypeDTO);
            return Ok(Result<PaymentTypeDTO?>.Success(result,"Cập nhật thành công"));
        }
        [HttpDelete("delete{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _paymentTypeService.DeleteAsync(id);
            return Ok(Result<bool>.Success(result,"Xóa thành công"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {
            var result = await _paymentTypeService.GetByIdAsync(id);
            return Ok(Result<PaymentTypeDTO?>.Success("Lấy thông tin thành công"));
        }
        [HttpGet("search")]
        public async Task<ActionResult<Result>> GetByNameAsync(int idStore, string name)
        {
            var results = await _paymentTypeService.GetByNameAsync(idStore, name);
            return Ok(results);
        }
        [HttpGet("all/{idStore:int}")]
        public async Task<ActionResult<Result>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var list = await _paymentTypeService.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, sortColumn, asc);
           
            return Ok(Result<PaginationResult<List<PaymentTypeDTO>>>.Success(list, "Lấy thông tin thành công"));
        }
    }
}
