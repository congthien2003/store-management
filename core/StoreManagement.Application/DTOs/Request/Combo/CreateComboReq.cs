namespace StoreManagement.Application.DTOs.Request.Combo
{
    public class CreateComboReq
    {
        public string Name { get; set; }
        public string Image { get; set; } = "";
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; } = true;
        public int IdStore { get; set; }
        public List<int> IdFoods { get; set; }
    }
}
