using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }   // Unique identifier for the item

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";

        public decimal Price { get; set; }
        public string Description { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        // Foreign key to Bundle
        public int? BundleId { get; set; }  // Making BundleId nullable Item belongs to one bundle
        public Bundle? Bundle { get; set; }  // Navigation property

        // Foreign key to Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }  // Navigation property

        // Other properties...
        [MaxLength(100)]
        public string SKU { get; set; } = "";

        [MaxLength(100)]
        public String CreatedBy { get; set; } = "";

        public int TotalQuantity { get; set; }
        [MaxLength(100)]
        public string ModelNumber { get; set; } = "";
        public int AvailableQuantity { get; set; }
        public decimal StockPrice { get; set; }
    }
}
