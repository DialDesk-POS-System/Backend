using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Entity tables (enums are NOT registered as DbSets)
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelPriceHistory> ModelPriceHistories { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<BulkImport> BulkImports { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<Return> Returns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default admin user
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin123", // Change after first login
                Role = UserRole.Admin
            });
        }
    }
}
