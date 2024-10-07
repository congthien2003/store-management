namespace StoreManagement.Application.DTOs.Request
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public bool Status { get; set; }
        public int TotalOrder { get; set; }
        public double Charge { get; set; }
        public int IdOrder { get; set; }
        public int IdPaymentType { get; set; }
        public int? IdVoucher { get; set; }
    }
}
