using StoreManagement.Application.DTOs.Response.RequestCallStaff;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System.Text.Json;

namespace StoreManagement.Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ISupabaseService _supabaseService;

        public RequestRepository(ISupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<dynamic> GetAllRequests()
        {
            var client = _supabaseService.GetClient();
            var response = await client.From<Request>().Get();
            // Kiểm tra nếu phản hồi không phải dạng List<Request>
            if (response is not null)
            {
                Console.WriteLine(response.Content);
                var jsonString = response.Content.ToString();
                var result = JsonSerializer.Deserialize<List<RequestCallStaffResponse>>(jsonString);// Convert object to JSON string
                return result;
            }

            return new List<Request>();
        }

        public async Task AddRequest(Request request)
        {
            var client = _supabaseService.GetClient();
            await client.From<Request>().Insert(request);
        }

        public async Task UpdateRequest(Request request)
        {
            var client = _supabaseService.GetClient();
            await client.From<Request>().Update(request);
        }
    }
}
