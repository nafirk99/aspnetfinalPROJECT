//using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Producta
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";

        //[Precision(16, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        // Foreign key to Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }  // Navigation property
    }
}
