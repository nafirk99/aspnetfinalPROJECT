using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web.Models
{
    public class TransferViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CurrentLocationId { get; set; }
        public int NewLocationId { get; set; }
        public IEnumerable<Location> AvailableLocations { get; set; }
    }
}
