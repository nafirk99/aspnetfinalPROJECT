using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public String Name { get; set; } = "";

        // Navigation Property For Related Products
        public ICollection<Producta> Products { get; set; }
    }
}
