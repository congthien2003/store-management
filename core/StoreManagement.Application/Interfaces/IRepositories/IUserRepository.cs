using StoreManagement.Application.DTOs.Auth;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Interfaces.IRepositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task<TUser> Delete(int id);
        Task<TUser> GetByEmail(string email, bool includeDeleted = false);

        Task<TUser> CreateUser(RegisterDTO registerDTO);
        Task<TUser> GetByLogin(string email, string password);
        Task<TUser> GetById(int id, bool includeDeleted = false);
        Task<TUser> Edit(TUser user);
        Task<TUser> UpdatePassword(int id, string password, bool includeDeleted = false);
    }
}
