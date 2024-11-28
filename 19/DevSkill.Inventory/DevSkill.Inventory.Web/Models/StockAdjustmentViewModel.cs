namespace DevSkill.Inventory.Web.Models
{
    public class StockAdjustmentViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int CurrentStock { get; set; }
        public int AvailableStock { get; set; }
        public int AdjustmentQuantity { get; set; }
    }
}
