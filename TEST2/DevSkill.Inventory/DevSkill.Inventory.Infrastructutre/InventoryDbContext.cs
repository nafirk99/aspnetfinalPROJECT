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

            // Product -> Location Relation (One-To-Many)
            modelBuilder.Entity<Producta>()
                .HasOne(p => p.Location)
                .WithMany(g => g.Products)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            // One-to-many relationship between Package and Producta (Asset)
            modelBuilder.Entity<Producta>()
                .HasOne(pr => pr.Package)
                .WithMany(pa => pa.Productas)
                .HasForeignKey(pr => pr.PackageId)
                .OnDelete(DeleteBehavior.Restrict);   // Restrict deletion if a Package has assets

            // One-to-many relationship between Bundle and Item
            modelBuilder.Entity<Item>()
                .HasOne(I => I.Bundle)
                .WithMany(B => B.Items)
                .HasForeignKey(I => I.BundleId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Product> products { get; set; }
        public DbSet<Producta> Productsa { get; set; }
        public DbSet<Category> Categories { get; set; }

        // New Colum for Assets
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Location> Locations { get; set; }

        // New DbSet added On 17th Oct
        public DbSet<Item> Items { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
    }
}
