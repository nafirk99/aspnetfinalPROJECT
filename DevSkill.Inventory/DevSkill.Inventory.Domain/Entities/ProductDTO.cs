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

        public IFormFile? ImageFile { get; set; }

        // Category handling
        [Required]
        public int CategoryId { get; set; }
    }
}
