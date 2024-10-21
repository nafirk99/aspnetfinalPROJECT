using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string PackageNumber { get; set; } = "";

        // List of assets that can be selected (with checkboxes)
        public List<AssetCheckboxViewModel> Assets { get; set; } = new List<AssetCheckboxViewModel>();
    }
}
