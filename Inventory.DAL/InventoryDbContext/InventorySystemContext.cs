using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DAL.InventoryDbContext
{
    public class InventorySystemContext : DbContext
    {
        public InventorySystemContext(DbContextOptions<InventorySystemContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
