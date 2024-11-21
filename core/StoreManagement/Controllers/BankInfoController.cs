using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response.BankInfo;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInfoController : ControllerBase
    {
        private readonly IBankInfoService _bankInfoService;
        
        public BankInfoController(IBankInfoService bankInfoService)
        {
            _bankInfoService = bankInfoService;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetBankInfo(int id)
        {
            var result = await _bankInfoService.Get(id);
            return Ok(Result<BankInfoResponse>.Success(result, "Lấy thành công"));
        }

        [HttpGet("get-list/{idStore:int}")]
        public async Task<ActionResult<Result>> GetBankInfoByIdStore(int idStore)
        {
            var result = await _bankInfoService.GetListByIdStore(idStore);
            return Ok(Result<List<BankInfoResponse>>.Success(result, "Lấy thành công"));
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreateBankInfo(BankInfoDTO req)
        {
            var result = await _bankInfoService.Create(req);
            return Ok(Result<BankInfoDTO>.Success(result, "Lấy thành công"));
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateBankInfo(BankInfoDTO req)
        {
            var result = await _bankInfoService.Update(req);
            return Ok(Result<BankInfoResponse>.Success(result, "Cập nhật thành công"));
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteBankInfo(int id)
        {
            var result = await _bankInfoService.Delete(id);
            return Ok(Result<bool>.Success(result, "Xóa thành công"));
        }
    }
}
