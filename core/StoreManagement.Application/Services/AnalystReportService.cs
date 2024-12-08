﻿using AutoMapper;
using DocumentFormat.OpenXml.VariantTypes;
using StoreManagement.Application.DTOs.Response.Analyst;
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

        public async Task<List<OrderByMonth>> GetMonthOrder(int idStore, int year)
        {
            var listOrder = await _orderRepository.GetMonthOrder(idStore, year);
            List<OrderByMonth> orders = new List<OrderByMonth>();
            for (int i = 0; i < listOrder.Count; i++)
            {
                var order = new OrderByMonth
                {
                    Month = i + 1,
                    Year = year,
                    total = listOrder[i]
                };
                orders.Add(order);
            }
            return orders;
        }

        public async Task<List<RevenueByMonth>> GetMonthRevenue(int idStore, int year)
        {
            var listRevenue = await _invoiceRepository.GetMonthRevenue(idStore, year);
            List<RevenueByMonth> list = new List<RevenueByMonth>();
            
            for(int i=0; i < listRevenue.Count; i++)
            {
                var revenue = new RevenueByMonth
                {
                    Month = i + 1,
                    Year = year,
                    Total = listRevenue[i],
                };
                list.Add(revenue);
            }
            return list;
        }

        public async Task<int> GetTableFree(int idStore)
        {
            var totalTable = await _tableRepository.GetCountTableFree(idStore);
            return totalTable;
        }

        
    }
}
