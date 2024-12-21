using StoreManagement.Domain.Enum;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.IRepositories
{
    public interface ITicketRepository<TTicket> where TTicket : Ticket
    {
        Task<TTicket> CreateAsync(TTicket ticket); 
        Task<TTicket> UpdateAsync(int id, TTicket ticket, bool incluDeleted = false);
        Task<TTicket> DeleteAsync(int id);
        Task<TTicket> GetByIdAsync(int id);
        Task<List<TTicket>> GetAllTicketAsync(ETicketStatus? status = null, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
        Task<List<TTicket>> GetTicketPending();
        Task<List<TTicket>> GetMyTicket(int idUser, ETicketStatus? status, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
    }
}
