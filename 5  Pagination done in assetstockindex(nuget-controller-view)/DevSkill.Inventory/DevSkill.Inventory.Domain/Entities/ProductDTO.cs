using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DevSkill.Inventory.Domain.Entities
{
    public class ProductDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string AIN { get; set; } = "";

        [Required]
        public String CreatedBy { get; set; } = "";

        public IFormFile? ImageFile { get; set; }

        // Category handling
        [Required]
        public int CategoryId { get; set; }

        // Vendor handling
        [Required]
        public int VendorId { get; set; }

        // Group Handling
        [Required]
        public int GroupId { get; set; }

        // Location Handling
        [Required]
        public int LocationId { get; set; }

        // New Columns
        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public string ModelNumber { get; set; } = "";
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public decimal StockPrice { get; set; }
    }
}
