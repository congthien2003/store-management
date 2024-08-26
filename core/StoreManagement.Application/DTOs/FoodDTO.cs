namespace StoreManagement.Application.DTOs
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Quantity { get; set; }
        public int IdCategory { get; set; }
    }
}
