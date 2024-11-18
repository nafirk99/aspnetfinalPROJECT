using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int BrandCount { get; set; }
        public int VendorCount { get; set; }
        public int GroupCount { get; set; }
        public int LocationCount { get; set; }
        public decimal AllAssetPrice { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
