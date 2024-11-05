using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class ConfirmationViewModel
    {
        public List<Package> Packages { get; set; } = new List<Package>();
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }  // Simple payment status
       
    }
}
