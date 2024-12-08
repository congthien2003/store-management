using Microsoft.AspNetCore.Mvc;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _repository;

        public RequestController(IRequestRepository repository)
        {
            _repository = repository;
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
    }
}
