namespace StoreManagement.Application.DTOs.Request.ComboItem
{
    public class CreateComboItemByListReq
    {
        public int IdCombo { get; set; }
        public int[] ListIdFood { get; set; }
    }
}
