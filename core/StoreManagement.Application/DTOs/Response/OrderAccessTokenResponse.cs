
namespace StoreManagement.Application.DTOs.Response
{
    public class OrderAccessTokenResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string QRURL { get; set; }
        public int? IdOrder { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime ExpirationTime { get; set; }
        public DateTime TimeCreated { get; set; }

    }
}
