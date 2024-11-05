using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Bundle
    {
        public int Id { get; set; }   // Unique identifier for the bundle

        [Required, MaxLength(100)]
        public string BundleNumber { get; set; } = "";  // Unique number to identify the bundle

        public DateTime CreatedAt { get; set; }

        // Navigation property for the one-to-many relationship with Item
        public List<Item> Items { get; set; } = new List<Item>();  // Collection of items in this bundle
    }
}
