using StoreManagement.Application.DTOs.Request;

namespace StoreManagement.Application.DTOs.Response.Combo
{
    public class ComboWithFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<FoodDTO> Foods { get; set; }
    }
}
