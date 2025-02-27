using Amazon.S3;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreManagement.Application.Interfaces.IWorkerService;

namespace StoreManagement.Worker.Worker
{
    public class WorkerSendMailMonthly : BackgroundService
    {
        private readonly ILogger<WorkerSendMailMonthly> _logger;
        private readonly ISendMailMonthly _workerService;
        
        public WorkerSendMailMonthly(ISendMailMonthly workerService, ILogger<WorkerSendMailMonthly> logger)
        {
            _workerService = workerService;
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
                await _workerService.DoWorkAsync();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(100000, stoppingToken);  // Delay tùy chỉnh giữa các lần thực thi
            }
        }
    }
}
