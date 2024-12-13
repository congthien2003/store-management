using StoreManagement.Application.Interfaces.IWorkerService;

namespace StoreManagement.Worker.WorkerService
{
    public class GetRevenue : IGetRevenue
    {
        /*private readonly IOrderRepository<Order> _orderRepository;

        public GetRevenue(IOrderRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }*/

        public async Task<dynamic> DoWorkAsync()
        {
            /*var order = await _context.Orders.Where(o => o.CreatedAt.Date == new DateTime(2024 - 11 - 04).Date && o.CreatedAt.Month == new DateTime(2024 - 11 - 04).Month).ToListAsync();*/
            Console.WriteLine("Worker get data from Order and Order Detail");
            var order = 1;
            return order;
        }
    }
}
