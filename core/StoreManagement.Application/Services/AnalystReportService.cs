using AutoMapper;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Services
{
    public class AnalystReportService : IAnalystReportService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IInvoiceRepository<Invoice> _invoiceRepository;
        private readonly ITableRepository<Table> _tableRepository;  

        public AnalystReportService(IMapper mapper, 
                                    IOrderRepository<Order> orderRepository,
                                    IInvoiceRepository<Invoice> invoiceRepository,
                                    ITableRepository<Table> tableRepository
                                    )
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
            _tableRepository = tableRepository;
        }

        public async Task<int> GetCountFoodSaleInDay(int idStore, DateTime dateTime)
        {
            var count = await _orderRepository.GetDailyFoodSaleAsync(idStore, dateTime);
            return count;
        }

        public async Task<int> GetCountOrderInDay(int idStore, DateTime dateTime)
        {
            var count = await _orderRepository.GetCountOrderInDay(idStore, dateTime);
            return count;
        }

        public async Task<double> GetDailyRevenueInDay(int idStore, DateTime dateTime)
        {
            var totalRevenue = await _invoiceRepository.GetDailyRevenueService(idStore, dateTime);
            return totalRevenue;
        }

        public async Task<int> GetTableFree(int idStore)
        {
            var totalTable = await _tableRepository.GetCountTableFree(idStore);
            return totalTable;
        }

        
    }
}
