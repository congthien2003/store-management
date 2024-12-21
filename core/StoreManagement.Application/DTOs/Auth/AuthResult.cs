namespace StoreManagement.Application.DTOs.Auth
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public int Role { get; set; }
        public string UserName { get; set; }
    }
}
