using Microsoft.Extensions.Configuration;
using StoreManagement.Application.Interfaces.IApiClientServices;
using Supabase;

namespace StoreManagement.Infrastructure.ApiClient
{
    public class SupabaseServices : ISupabaseService
    {
        private readonly Client _supabaseClient;

        public SupabaseServices(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];

            // Khởi tạo Supabase Client
            _supabaseClient = new Client(url, key);
            _supabaseClient.InitializeAsync().Wait();
        }

        public Client GetClient()
        {
            return _supabaseClient;
        }
    }
}
