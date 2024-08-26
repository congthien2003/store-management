using AutoMapper;
using StoreManagement.DataAccess.Data;
using StoreManagement.Domain.Interfaces.IRepositories;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;

        public UserRepository(DataContext dataContext,
                              IMapper mapper)
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _dataContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            user.IsDeleted = true;
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Edit(User user)
        {
            var exists = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (exists == null)
            {
                return null;
            }
            else
            {
                exists.Email = user.Email;
                exists.Username = user.Username;
                exists.Phones = !string.IsNullOrEmpty(user.Phones) ? user.Phones : "";
                exists.Role = user.Role;
            }
            await _dataContext.SaveChangesAsync();
            return exists;
        }

        public async Task<User> GetById(int id, bool includeDeleted = false)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
        }

        public async Task<User> GetByEmail(string email, bool includeDeleted = false)
        {
            var user = await _dataContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetByLogin(string email, string password)
        {
            var user = await _dataContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> CreateUser(string email, string username, string password)
        {
            var obj = new { email, username, password };
            var newUser = mapper.Map<User>(obj);
            _dataContext.Users.Add(newUser);
            await _dataContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> UpdatePassword(int id, string password, bool includeDeleted = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.Password = password;
            _dataContext.Update(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
