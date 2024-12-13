using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface ITicketService
    {
        Task<TicketDTO> CreateAsync(TicketDTO ticketDTO);
        Task<TicketDTO> UpdateAsync(int id,TicketDTO ticketDTO);
        Task<bool> DeleteAsync(int id);
        Task<TicketResponse>GetTicketById(int id);
        Task<List<TicketResponse>> GetTicketPending();
        Task<PaginationResult<List<TicketResponse>>> GetAllTicketAsync(ETicketStatus? status = null, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, bool incluDeleted = false);
        Task<PaginationResult<List<TicketResponse>>> GetMyTicket(int idUser, ETicketStatus? status,  string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, bool incluDeleted = false);
    }
}
