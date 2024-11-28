using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Models
{
    public class StockTransferViewModel
    {
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<ProductTransferViewModel> ProductList { get; set; }
    }
}
