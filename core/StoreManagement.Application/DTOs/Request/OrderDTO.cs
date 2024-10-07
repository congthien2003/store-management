namespace StoreManagement.Application.DTOs.Request
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string NameUser { get; set; }
        public string PhoneUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdTable { get; set; }

    }
}
