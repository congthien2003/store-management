using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.ApiClient.Chat;
using StoreManagement.Application.DTOs.ApiClient.FlaskAPI;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IQRServices qRServices;
        private readonly IGoogleAPI googleAPI;
        private readonly IOrderDetailService orderDetailService;
        private readonly IFlaskAPI flaskApi;
        public ClientController(IQRServices qRServices,
                                IGoogleAPI googleAPI,
                                IOrderDetailService orderDetailService,
                                IFlaskAPI flaskApi)
        {
            this.qRServices = qRServices;
            this.googleAPI = googleAPI;
            this.orderDetailService = orderDetailService;
            this.flaskApi = flaskApi;
        }

        [HttpPost("ChatGemini")]
        public async Task<dynamic> ChatBox(ChatReq req)
        {
            var result = await googleAPI.Gemini(req.promt);
            return Ok(Result<dynamic>.Success(result, "Success"));
        }

        [HttpPost("GetPopularComboFoods")]
        public async Task<dynamic> GetPopularComboFoods(GetPopularCombo req)
        {
            var list = await orderDetailService.GetFoodOrderDetailDataByIdStore(req.IdStore);
            var result = await flaskApi.GetPopularComboAsync(list, req.IdStore);
            return Ok(result);
        }
    }
}
