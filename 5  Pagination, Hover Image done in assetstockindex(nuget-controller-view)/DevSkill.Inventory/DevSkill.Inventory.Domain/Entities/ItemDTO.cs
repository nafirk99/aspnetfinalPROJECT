using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class ItemDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string SKU { get; set; } = "";
        [Required]
        public String CreatedBy { get; set; } = "";
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
