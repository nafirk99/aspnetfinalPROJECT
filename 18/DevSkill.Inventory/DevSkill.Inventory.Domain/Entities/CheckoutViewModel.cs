using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class CheckoutViewModel
    {
        public List<int> SelectedPackageIds { get; set; } = new List<int>();
        public decimal TotalAmount { get; set; }

        // Add the following property to hold the package details
        public List<Package> Packages { get; set; } = new List<Package>();
    }
}
