using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class AssetCheckboxViewModel
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public bool IsSelected { get; set; }  // To check if the asset is selected for this package
    }
}
