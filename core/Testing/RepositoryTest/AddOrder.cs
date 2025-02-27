using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using StoreManagement.Infrastructure.Repositories;

namespace Testing.RepositoryTest
{
    public class AddOrder : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly DataContext _dataContext;
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IConfiguration configuration;
        public AddOrder()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

            _dataContext = new DataContext(options);
            _orderRepository = new OrderRepositoy(_dataContext);
        }

        [Fact]
        public async void AddOrderTest()
        {
            Order order = new Order();
            order.Guid = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.IsDeleted = false;
            order.IdInvoice = 0;
            order.hasInvoice = false;
            order.IdTable = 1;
            order.Status = false;

            var newOrder = await _orderRepository.CreateAsync(order);

            Assert.True(order.IdTable != 0);
            Assert.True(newOrder.IdTable == order.IdTable);
            Assert.True(newOrder.Guid != Guid.Empty);
        }
    }
}
