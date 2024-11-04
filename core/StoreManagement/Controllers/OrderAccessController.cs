using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Application.RealTime;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAccessController : ControllerBase
    {
        private readonly IOrderAccessService _orderAccessService;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderAccessController(IOrderAccessService orderAccessService, IHubContext<OrderHub> hubContext)
        {
            _orderAccessService = orderAccessService;
            _hubContext = hubContext;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Result>> GetById(Guid id) 
        { 

            return Ok(Result<OrderAccessTokenResponse?>.Success(await _orderAccessService.Get(id), "Lấy thành công"));
        }

        [HttpGet("URL")]
        public async Task<ActionResult<Result>> GetByURL(string idTable, string idStore)
        {
            await _hubContext.Clients.Group(idStore).SendAsync("ReceiveNotificationAccessTable", idTable);
            OrderAccessTokenResponse result = await _orderAccessService.Request(idTable);
            return Ok(Result<OrderAccessTokenResponse?>.Success(result, "Lấy thành công"));
        }

        [HttpGet("Request")]
        public async Task RequestAccess(string idTable, string idStore)
        {
            await _hubContext.Clients.Group(idStore).SendAsync("RequestAccess", idTable);
            // sau khi truy cập, sẽ tạo
            /*OrderAccessTokenDTO orderAccessTokenDTO = new OrderAccessTokenDTO();
            orderAccessTokenDTO.QRURL = idTable;
            orderAccessTokenDTO.IdOrder = null;
            orderAccessTokenDTO.IsActive = true;
            await _orderAccessService.Create(orderAccessTokenDTO);*/
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateAsync(OrderAccessTokenDTO orderAccessTokenDTO)
        {
            return Ok(Result<OrderAccessTokenResponse?>.Success(await _orderAccessService.Create(orderAccessTokenDTO), "Tạo thành công"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Result>> Create(Guid id, OrderAccessTokenDTO orderAccessTokenDTO)
        {
            return Ok(Result<OrderAccessTokenResponse?>.Success(await _orderAccessService.Update(id, orderAccessTokenDTO), "Cập nhật thành công"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Result>> DeleteAsync(Guid id)
        {
            return Ok(Result<OrderAccessTokenResponse?>.Success(await _orderAccessService.Delete(id), "Xóa thành công"));
        }
    }
}
