using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using StoreManagement.Application.DTOs.Response.Analyst;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services
{
    public class AnalystReportService : IAnalystReportService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IInvoiceRepository<Invoice> _invoiceRepository;
        private readonly ITableRepository<Table> _tableRepository;
        private readonly IProductSellRepository<ProductSell> _productSellRepository;

        public AnalystReportService(IMapper mapper,
                                    IOrderRepository<Order> orderRepository,
                                    IInvoiceRepository<Invoice> invoiceRepository,
                                    ITableRepository<Table> tableRepository,
                                    IProductSellRepository<ProductSell> productSellRepository
                                    )
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
            _tableRepository = tableRepository;
            _productSellRepository = productSellRepository;
        }

        public async Task<AvgFoodOneMonth> GetAvgFoodPerOrderOneMonth(int idStore, int month, int year)
        {
            var avgFoodPerOrder = await _orderRepository.GetAVGFoodPerOrderOneMonthAsync(idStore, month, year);
            var result = new AvgFoodOneMonth
            {
                Year = year,
                Month = month,
                Average = avgFoodPerOrder
            };
            return result;
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

        public async Task<List<FoodByMonth>> GetMonthFood(int idStore, int year)
        {
            List<FoodByMonth> foods = new List<FoodByMonth>();


            for (int month = 1; month <= 12; month++)
            {
                int totalFood = await _orderRepository.GetMonthFoodAsync(idStore, month, year);

                foods.Add(new FoodByMonth
                {
                    Month = month,
                    Year = year,
                    Quantity = totalFood
                });
            }

            return foods;
        }

        public async Task<List<OrderByMonth>> GetMonthOrder(int idStore, int year)
        {
            var ordersByMonth = new List<OrderByMonth>();


            for (int month = 1; month <= 12; month++)
            {
                int totalOrders = await _orderRepository.GetMonthOrderAsync(idStore, month, year);

                var orderByMonth = new OrderByMonth
                {
                    Month = month,
                    Year = year,
                    total = totalOrders
                };

                ordersByMonth.Add(orderByMonth);
            }

            return ordersByMonth;
        }

        public async Task<List<RevenueByMonth>> GetMonthRevenue(int idStore, int year)
        {
            var revenueList = new List<RevenueByMonth>();

            for (int month = 1; month <= 12; month++)
            {
                double revenue = await _invoiceRepository.GetMonthRevenueAsync(idStore, month, year);

                var monthlyRevenue = new RevenueByMonth
                {
                    Month = month,
                    Year = year,
                    Total = revenue
                };

                revenueList.Add(monthlyRevenue);
            }

            return revenueList;
        }

        public async Task<int> GetTableFree(int idStore)
        {
            var totalTable = await _tableRepository.GetCountTableFree(idStore);
            return totalTable;
        }

        public async Task<List<DataByMonth>> GetTotalProductSell(int idStore, int year)
        {
            List<DataByMonth> list = new List<DataByMonth>();
            for (var i = 1; i < 13; i++)
            {
                var total = await _productSellRepository.GetTotalProductSellByMonth(1, year);
                DataByMonth temp = new DataByMonth { Month = i, Year = year, total = total };
                list.Add(temp);
            }
            return list;
        }
        public async Task<List<MonthlyReport>> monthlyReports(int idStore)
        {
            var reports = new List<MonthlyReport>();


            DateTime now = DateTime.Now;
            for (int i = 1; i <= 3; i++)
            {
                DateTime monthDate = now.AddMonths(-i);
                int month = monthDate.Month;
                int year = monthDate.Year;

                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                double revenue = await _invoiceRepository.GetMonthRevenueAsync(idStore, month, year);
                int totalOrder = await _orderRepository.GetMonthOrderAsync(idStore, month, year);
                var avgFoodPerOrder = await _orderRepository.GetAVGFoodPerOrderOneMonthAsync(idStore, month, year);
                var totalFoodSold = await _orderRepository.GetMonthFoodAsync(idStore, month, year);
                int totalDays = DateTime.DaysInMonth(year, month);
                reports.Add(new MonthlyReport
                {
                    IdStore = idStore,
                    Month = month,
                    Year = year,
                    TotalRevenue = revenue,
                    TotalFoodSold = totalFoodSold,
                    TotalOrders = totalOrder,
                    AvgOrderValue = avgFoodPerOrder,
                    TotalDay = totalDays
                });

            }
            return reports;
        }

        public async Task<List<MonthlySummaryReport>> monthlySummaryReports(int idStore)
        {
            var reports = new List<MonthlySummaryReport>();

            DateTime now = DateTime.Now;
            int year = now.Year;
            var months = new[] { now.AddMonths(-1), now.AddMonths(-2), now.AddMonths(-3) };
            var data = new List<dynamic>();
            foreach (var monthDate in months)
            {
                int month = monthDate.Month;
                int yearOfMonth = monthDate.Year;
                int daysInMonth = DateTime.DaysInMonth(yearOfMonth, month);

                var monthlyData = new
                {
                    Month = month,
                    Year = yearOfMonth,
                    Revenue = await _invoiceRepository.GetMonthRevenueAsync(idStore, month, year),
                    TotalOrder = await _orderRepository.GetMonthOrderAsync(idStore, month, year),
                    AvgFoodPerOrder = await _orderRepository.GetAVGFoodPerOrderOneMonthAsync(idStore, month, year),
                    TotalFoodSold = await _orderRepository.GetMonthFoodAsync(idStore, month, year),
                    DaysInMonth = daysInMonth
                };

                data.Add(monthlyData);
            }
            reports.Add(new MonthlySummaryReport
            {
                ShopID = idStore,
                Year = year,
                Month1 = data[0].Month,
                Month2 = data[1].Month,
                Month3 = data[2].Month,
                TotalRevenueMonth1 = data[0].Revenue,
                TotalRevenueMonth2 = data[1].Revenue,
                TotalRevenueMonth3 = data[2].Revenue,
                TotalFoodSoldMonth1 = data[0].TotalFoodSold,
                TotalFoodSoldMonth2 = data[1].TotalFoodSold,
                TotalFoodSoldMonth3 = data[2].TotalFoodSold,
                TotalOrderMonth1 = data[0].TotalOrder,
                TotalOrderMonth2 = data[1].TotalOrder,
                TotalOrderMonth3 = data[2].TotalOrder,
                AvgFoodPerOrderMonth1 = data[0].AvgFoodPerOrder,
                AvgFoodPerOrderMonth2 = data[1].AvgFoodPerOrder,
                AvgFoodPerOrderMonth3 = data[2].AvgFoodPerOrder,
                DaysInMonth1 = data[0].DaysInMonth,
                DaysInMonth2 = data[1].DaysInMonth,
                DaysInMonth3 = data[2].DaysInMonth,
                Quarter1 = GetQuarter(data[0].Month),
                Quarter2 = GetQuarter(data[1].Month),
                Quarter3 = GetQuarter(data[2].Month),
            });

            return reports;
        }

        private string GetQuarter(int month)
        {
            return month switch
            {
                <= 3 => "Q1",
                <= 6 => "Q2",
                <= 9 => "Q3",
                _ => "Q4",
            };
        }
    }
}
