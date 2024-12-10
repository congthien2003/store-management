using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StoreManagement.Application.RealTime;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _repository;
        private readonly IHubContext<OrderHub> _hubContext;

        public RequestController(IRequestRepository repository, IHubContext<OrderHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(int IdTable)
        {
            Request request = new Request();
            request.TableNumber = IdTable.ToString();
            request.RequestTime = DateTime.UtcNow;
            request.Status = "pending";

            await _repository.AddRequest(request);
            return Ok(new { Message = "Request added successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            var requests = await _repository.GetAllRequests();
            return Ok(requests);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, Request request)
        {
            request.Id = id;
            await _repository.UpdateRequest(request);
            return Ok(new { Message = "Request updated successfully" });
        }

        [HttpPost("RequestCallStaff")]
        public async void RequestCallStaff(int idStore, int idTable)
        {
            await _hubContext.Clients.Group(idStore.ToString()).SendAsync("ReceiveNotification", $"Bàn ${idTable} gửi yêu cầu thanh toán !");
        }
    }
}
