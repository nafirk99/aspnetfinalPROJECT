namespace DevSkill.Inventory.Web.Models
{
    public class ProductTransferViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AvailableStock { get; set; }
        public int TransferQuantity { get; set; }
    }
}
