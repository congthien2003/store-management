using Amazon.S3;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreManagement.Application.Interfaces.IWorkerService;

namespace StoreManagement.Worker.Worker
{
    public class WorkerGetDataRevenue : BackgroundService
    {
        private readonly ILogger<WorkerGetDataRevenue> _logger;
        private readonly IGetRevenue _workerService;
        private readonly IAmazonS3 _s3Client;
        public WorkerGetDataRevenue(IGetRevenue workerService, ILogger<WorkerGetDataRevenue> logger, IAmazonS3 s3Client)
        {
            _workerService = workerService;
            _logger = logger;
            _s3Client = s3Client;

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
                var order = await _workerService.DoWorkAsync();
                Console.WriteLine(order.ToString());
                _logger.LogInformation("Get Order Success");

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _workerService.DoWorkAsync();
                await Task.Delay(100000, stoppingToken);  // Delay tùy chỉnh giữa các lần thực thi
            }
        }
    }
}
