namespace StoreManagement.Application.DTOs.Response
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RequestBy { get; set; }
    }
}
