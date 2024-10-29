using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Domain.Entities
{
    public class CategoryDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
    }
}
