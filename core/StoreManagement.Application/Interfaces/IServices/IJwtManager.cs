using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IJwtManager
    {
        string CreateToken(UserDTO user);
        string getHashpassword(string password);
    }
}
