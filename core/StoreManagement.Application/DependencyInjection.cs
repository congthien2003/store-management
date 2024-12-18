using Amazon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Application.Services;
using StoreManagement.Domain.Enum;
using StoreManagement.Infrastructure.ApiClient;
using StoreManagement.Services;
namespace StoreManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // add scoped services
            services.AddSignalR();
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IOrderSerivce, OrderService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IProductSellService, ProductSellService>();
            services.AddTransient<IOrderAccessService, OrderAccessService>();
            services.AddTransient<IAnalystReportService, AnalystReportService>();
            services.AddTransient<IKPIService, KPIService>();
            services.AddTransient<IBankInfoService, BankInfoService>();
            services.AddTransient<IComboService, ComboService>();
            services.AddTransient<IComboItemService, ComboItemService>();
            services.AddTransient<ITicketService, TicketSerivce>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IEmailService>(provider =>
            {
                var awsSesConfig = provider.GetRequiredService<IOptions<AwsSesConfig>>().Value;
                var exportExcellService = provider.GetRequiredService<IExportExcellService>();
                var storeService = provider.GetRequiredService<IStoreService>();
                return new AwsSesEmailService(
                    awsAccessKey: awsSesConfig.AccessKey,
                    awsSecretKey: awsSesConfig.SecretKey,
                    senderEmail: awsSesConfig.SenderEmail,
                    region: RegionEndpoint.GetBySystemName(awsSesConfig.Region),
                    exportExcellService,
                    storeService
                );
            });
            return services;

        }
    }
}
