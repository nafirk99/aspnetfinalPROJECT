using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DevSkill.Inventory.Infrastructutre
{
    public class InventoryDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public InventoryDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product -> Category relation (One-to-Many)
            modelBuilder.Entity<Producta>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);  // This prevents category deletion if products exist

            // Product -> Vendor relation (One-to-many)
            modelBuilder.Entity<Producta>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product -> Group Relation (One-To-Many)
            modelBuilder.Entity<Producta>()
                .HasOne(p => p.Group)
                .WithMany(g => g.Products)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Product> products { get; set; }
        public DbSet<Producta> Productsa { get; set; }
        public DbSet<Category> Categories { get; set; }

        // New Colum for Assets
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
