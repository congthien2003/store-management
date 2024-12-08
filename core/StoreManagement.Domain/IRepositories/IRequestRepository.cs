using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IRequestRepository
    {
        Task<dynamic> GetAllRequests();
        Task AddRequest(Request request);
        Task UpdateRequest(Request request);
    }
}
