using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Application.DTOs.ApiClient.AWS;
using StoreManagement.Application.Interfaces.IWorkerService;
using StoreManagement.Infrastructure.ApiClient;

namespace StoreManagement.Worker.WorkerService
{
    public class SendMailMonthly : ISendMailMonthly
    {
        private readonly IServiceProvider _serviceProvider;

        public SendMailMonthly(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DoWorkAsync()
        {
            DateTime date = DateTime.Now;
            if (date.Day == 14)
            {
                Console.WriteLine($"Send mail periodic month {date.Month}");
                using (var scope = _serviceProvider.CreateScope())
                {

                    EmailMonthlyReport req = new EmailMonthlyReport
                    {
                        RecipientEmail = "nhoccuthien0538@gmail.com",
                        IdStore = 1,
                        StartDate = "01/11/2024",
                        EndDate = "01/12/2024",
                    };
                    var service = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    await service.SendMailMonthlyReport(req);
                }
            }
            else
            {
                Console.WriteLine("Not in this day");
            }
        }
    }
}
