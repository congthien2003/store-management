namespace StoreManagement.Application.DTOs.Request.Store
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int IdUser { get; set; }
    }
}
