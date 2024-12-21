using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.Enum;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreatAsync(TicketDTO ticketDTO)
        {
            var result = await _ticketService.CreateAsync(ticketDTO);
            return Ok(Result<TicketDTO?>.Success(result,"Tạo mới ticket thành công"));
        }
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result>> UpdateAsync(int id,TicketDTO ticketDTO)
        {
            var result = await _ticketService.UpdateAsync(id,ticketDTO);
            return Ok(Result<TicketDTO?>.Success(result, "Cập nhật ticket thành công"));
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result>> DeleteAsync(int id)
        {
            var result = await _ticketService.DeleteAsync(id);
            return Ok(Result<bool?>.Success(result,"Xóa thành công"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetByIdAsync(int id)
        {
            var result = await _ticketService.GetTicketById(id);
            return Ok(Result<TicketResponse?>.Success(result,"Lấy thông tin thành công"));
        }
        [HttpGet("all/{status:int}")]
        public async Task<ActionResult<Result>> GetAllTicket(ETicketStatus? status = null, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false)
        {
            var tickets = await _ticketService.GetAllTicketAsync(status,currentPage,pageSize,searchTerm,sortColumn,asc, filter);
            return Ok(Result<PaginationResult<List<TicketResponse>?>>.Success(tickets, "Lấy thông tin thành công"));
        }
        [HttpGet("my-ticket/{idUser:int}/{status:int}")]
        public async Task<ActionResult<Result>> GetAllMyTicket(int idUser, ETicketStatus? status,string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false)
        {
            var tickets = await _ticketService.GetMyTicket(idUser,status, currentPage, pageSize, searchTerm, sortColumn, asc, filter);
            return Ok(Result<PaginationResult<List<TicketResponse>?>>.Success(tickets, "Lấy thông tin thành công"));
        }
    }
}
