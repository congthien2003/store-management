using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.EntityFrameworkCore;
using Mscc.GenerativeAI;
using StoreManagement.Domain.Enum;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository<Ticket>
    {
        private readonly DataContext _dataContext;

        public TicketRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            var ticketExist = await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Title == ticket.Title && x.IsDeleted == false);
            if (ticketExist != null)
            {
                throw new Exception("Ticket đã tồn tại");
            }
            var newTicket = await _dataContext.Tickets.AddAsync(ticket);
            await _dataContext.SaveChangesAsync();
            return newTicket.Entity;
        }

        public async Task<Ticket> DeleteAsync(int id)
        {
            var ticket = await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Bàn không tồn tại");
            }
            ticket.Status = 3;
            _dataContext.Tickets.Update(ticket);
            await _dataContext.SaveChangesAsync();
            return ticket;
        }

        public async Task<List<Ticket>> GetAllTicketAsync(ETicketStatus? status = null, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var query = _dataContext.Tickets.AsQueryable();
            if (status.HasValue)
            {
                int statusValue = (int)status.Value;
                query = query.Where(x => x.Status == statusValue);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.Title.Contains(searchTerm) || x.Description.Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                query = query.Where(x =>x.Title.Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    query = query.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    query = query.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = query.ToListAsync();
            return await list;
        }
        public Expression<Func<Ticket, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "name":
                    return x => x.Title;
                default:
                    return x => x.Id;
            }
        }
        public async Task<Ticket> GetByIdAsync(int id)
        {
            var ticket = await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket không tồn tại");
            }
            return ticket;
        }

        public async Task<List<Ticket>> GetTicketPending()
        {
            var list = await _dataContext.Tickets.Where(x => x.Status == 0).ToListAsync();
            return list;
        }

        public async Task<Ticket> UpdateAsync(int id, Ticket ticket, bool incluDeleted = false)
        {
            var ticketUpdate = await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (ticketUpdate == null)
            {
                throw new KeyNotFoundException("Ticket không tồn tại");
            }
            ticketUpdate.Status = ticket.Status;
            _dataContext.Tickets.Update(ticketUpdate);
            await _dataContext.SaveChangesAsync();
            return ticketUpdate;
        }

        public async Task<List<Ticket>> GetMyTicket(int idUser, ETicketStatus? status, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var query = _dataContext.Tickets.AsQueryable();
            if (idUser != null && status.HasValue)
            {
                int statusValue = (int)status.Value;
                query = query.Where(x => x.RequestBy == idUser && x.Status == statusValue);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.Title.Contains(searchTerm) || x.Description.Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                query = query.Where(x => x.Title.Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    query = query.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    query = query.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = query.ToListAsync();
            return await list;
        }
    }
}
