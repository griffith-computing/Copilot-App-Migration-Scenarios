using System;
using System.Data.Entity.Migrations;
using ProductsDesktop.Models;

namespace ProductsDesktop.Migrations
{
    /// <summary>
    /// EF6 Code-First Migrations configuration. Enables automatic migration
    /// on app start (see Program.cs) and seeds a handful of sample products
    /// so the demo has data out of the box.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductsContext context)
        {
            context.Products.AddOrUpdate(
                p => p.Name,
                new Product { Name = "Widget", Category = "Hardware", Price = 9.99m, InStock = true, CreatedDate = new DateTime(2024, 1, 15) },
                new Product { Name = "Gadget", Category = "Hardware", Price = 24.50m, InStock = true, CreatedDate = new DateTime(2024, 2, 3) },
                new Product { Name = "Gizmo", Category = "Electronics", Price = 149.00m, InStock = false, CreatedDate = new DateTime(2024, 3, 22) },
                new Product { Name = "Doohickey", Category = "Miscellaneous", Price = 3.25m, InStock = true, CreatedDate = new DateTime(2024, 4, 10) });
        }
    }
}
