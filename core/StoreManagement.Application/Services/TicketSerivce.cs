using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.Enum;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Services
{
    public class TicketSerivce : ITicketService
    {
        private readonly ITicketRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;

        public TicketSerivce(ITicketRepository<Ticket> ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;  
        }

        public async Task<TicketDTO> CreateAsync(TicketDTO ticketDTO)
        {
            var ticket = _mapper.Map<Ticket>(ticketDTO);
            var ticketCreated = await _ticketRepository.CreateAsync(ticket);
            return _mapper.Map<TicketDTO>(ticketCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _ticketRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<TicketResponse>> GetAllTicketAsync(ETicketStatus? status = null)
        {
            var ticket = await _ticketRepository.GetAllTicketAsync(status);
            return _mapper.Map<List<TicketResponse>>(ticket);
        }

        public async Task<PaginationResult<List<TicketResponse>>> GetAllTicketAsync(ETicketStatus? status = null, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, bool incluDeleted = false)
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            var list = await _ticketRepository.GetAllTicketAsync(status, searchTerm, sortColumn, asc);
            var count = list.Count;
            list = list.Skip(_currentPage * _pageSize - _pageSize).Take(_pageSize).ToList();
            var listTicket = _mapper.Map<List<TicketResponse>>(list);
            return PaginationResult<List<TicketResponse>>.Create(listTicket, _currentPage, _pageSize, count);
        }

        public async Task<PaginationResult<List<TicketResponse>>> GetMyTicket(int idUser, ETicketStatus? status, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, bool incluDeleted = false)
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            var list = await _ticketRepository.GetMyTicket(idUser, status, searchTerm, sortColumn, asc);
            var count = list.Count;
            list = list.Skip(_currentPage * _pageSize - _pageSize).Take(_pageSize).ToList();
            var listTicket = _mapper.Map<List<TicketResponse>>(list);
            return PaginationResult<List<TicketResponse>>.Create(listTicket, _currentPage, _pageSize, count);
        }

        public async Task<TicketResponse> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return _mapper.Map<TicketResponse>(ticket);
        }

        public async Task<List<TicketResponse>> GetTicketPending()
        {
            var ticket = await _ticketRepository.GetTicketPending();
            return _mapper.Map<List<TicketResponse>>(ticket);
        }

        public async Task<TicketDTO> UpdateAsync(int id, TicketDTO ticketDTO)
        {
            var ticketUpdate = _mapper.Map<Ticket>(ticketDTO);
            var update = await _ticketRepository.UpdateAsync(id, ticketUpdate);
            return _mapper.Map<TicketDTO>(update);
        }
    }
}
