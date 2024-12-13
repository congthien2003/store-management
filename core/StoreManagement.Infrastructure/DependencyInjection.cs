using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.ApiClient;
using StoreManagement.Infrastructure.Data;
using StoreManagement.Infrastructure.Repositories;
using StoreManagement.Infrastructure.Services;
namespace StoreManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });
            // add scoped repository
            services.AddScoped<IUserRepository<User>, UserRepository>();
            services.AddScoped<IStoreRepository<Store>, StoreRepository>();
            services.AddScoped<ICategoryRepository<Category>, CategoryRepository>();
            services.AddScoped<IFoodRepository<Food>, FoodRepository>();
            services.AddScoped<ITableRepository<Table>, TableRepository>();
            services.AddScoped<IPaymentTypeRepository<PaymentType>, PaymentTypeRepository>();
            services.AddScoped<IOrderRepository<Order>, OrderRepositoy>();
            services.AddScoped<IOrderDetailRepository<OrderDetail>, OrderDetailRepository>();
            services.AddScoped<IInvoiceRepository<Invoice>, InvoiceRepository>();
            services.AddScoped<IProductSellRepository<ProductSell>, ProductSellRepository>();
            services.AddScoped<IOrderAccessTokenRepository<OrderAccessToken>, OrderAccessTokenRepository>();
            /*services.AddScoped<IVoucherRepository<Voucher>, VoucherRepository>();*/
            services.AddScoped<IKPIRepository<KPI>, KPIRepository>();
            services.AddScoped<IBankInfoRepository<BankInfo>, BankInfoRepository>();
            services.AddScoped<ITicketRepository<Ticket>, TicketRepository>();
            // Register Client Service
            services.AddTransient<IQRServices, QRService>();
            services.AddTransient<IExportExcellService, ExportExcellService>();
            services.AddTransient<IGoogleAPI, GoogleAPI>();
            return services; 

        }
    }
}
