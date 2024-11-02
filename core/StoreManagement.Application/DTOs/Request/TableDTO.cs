namespace StoreManagement.Application.DTOs.Request
{
    public class TableDTO
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; }
        public int IdStore { get; set; }

    }
}
