using StoreManagement.Application.DTOs.Request;

namespace StoreManagement.Application.DTOs.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasInvoice { get; set; }
        public TableDTO Table { get; set; }
        public bool Status { get; set; }
    }
}
