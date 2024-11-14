using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task<TUser> Delete(int id);
        Task<TUser> GetByEmail(string email, bool includeDeleted = false);
        Task<List<TUser>> GetAll(string searchTerm = "", string sortCol = "", bool ascSort = true, int? role = null, bool incluDeleted = false);
        Task<TUser> CreateUser(User user);
        Task<TUser> GetByLogin(string email, string password);
        Task<TUser> GetById(int id, bool includeDeleted = false);
        Task<TUser> Edit(TUser user);
        Task<TUser> UpdatePassword(int id, string password, bool includeDeleted = false);
        Task<List<TUser>> GetAllUserResponse(string searchTerm = "", string sortCol = "", bool ascSort = true, int? role = 1, bool incluDeleted = false);
    }
}
