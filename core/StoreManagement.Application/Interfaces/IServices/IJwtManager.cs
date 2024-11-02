using StoreManagement.Application.DTOs.Request;
using System.Security.Claims;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IJwtManager
    {
        string CreateToken(UserDTO user);
        string getHashpassword(string password);
        ClaimsPrincipal ValidateToken(string token);
    }
}
