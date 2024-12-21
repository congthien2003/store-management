namespace StoreManagement.Application.DTOs.Request.Combo
{
    public class ComboDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public int IdStore { get; set; }
    }
}
