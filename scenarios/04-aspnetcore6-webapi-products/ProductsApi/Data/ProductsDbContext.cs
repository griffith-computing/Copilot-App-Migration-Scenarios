using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        /// <summary>
        /// This sample intentionally uses <c>EnsureCreated()</c> at startup
        /// (see <c>Startup.Configure</c>) instead of EF Core Migrations --
        /// a common shortcut in small services/prototypes. A real migration
        /// to .NET 10 is a good time to introduce proper EF Core Migrations
        /// (or idempotent SQL scripts) instead of carrying this forward.
        /// </summary>
        public static void SeedIfEmpty(ProductsDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            context.Products.AddRange(
                new Product { Name = "Wireless Mouse", Category = "Accessories", Price = 24.99m, InStock = true, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Mechanical Keyboard", Category = "Accessories", Price = 89.50m, InStock = true, CreatedDate = DateTime.UtcNow },
                new Product { Name = "27-inch Monitor", Category = "Displays", Price = 249.00m, InStock = false, CreatedDate = DateTime.UtcNow });

            context.SaveChanges();
        }
    }
}
