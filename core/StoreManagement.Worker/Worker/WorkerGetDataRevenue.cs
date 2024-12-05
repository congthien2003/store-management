using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StoreManagement.Worker.Worker
{
    public class WorkerGetDataRevenue : BackgroundService
    {
        private readonly ILogger<WorkerGetDataRevenue> _logger;
        /*private readonly IGetRevenue _workerService;*/
        public WorkerGetDataRevenue(ILogger<WorkerGetDataRevenue> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker started");

            while (!cancellationToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker is running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopped");
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                /*var order = await _workerService.DoWorkAsync();*/
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                /*await _workerService.DoWorkAsync();*/
                await Task.Delay(100000, stoppingToken);  // Delay tùy chỉnh giữa các lần thực thi
            }
        }
    }
}
