using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        // Navigation property for the relationship
        public List<Producta> Products { get; set; }
    }
}
