using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Package
    {
        public int Id { get; set; }   // Unique identifier for the package

        [Required, MaxLength(100)]
        public string PackageNumber { get; set; } = "";  // Unique number to identify the package

        public DateTime CreatedAt { get; set; }

        // Navigation property for the one-to-many relationship with Producta (Assets)
        public List<Producta> Productas { get; set; } = new List<Producta>();  // Collection of assets in this package
    }
}
