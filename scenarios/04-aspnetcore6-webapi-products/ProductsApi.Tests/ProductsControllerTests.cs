using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Controllers;
using ProductsApi.Data;
using ProductsApi.Models;
using Xunit;

namespace ProductsApi.Tests
{
    public class ProductsControllerTests
    {
        private static ProductsDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new ProductsDbContext(options);
        }

        [Fact]
        public async Task GetProducts_ReturnsSeededProducts()
        {
            using var context = CreateContext(nameof(GetProducts_ReturnsSeededProducts));
            ProductsDbContext.SeedIfEmpty(context);
            var controller = new ProductsController(context);

            var result = await controller.GetProducts();

            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(result.Value);
            Assert.Equal(3, products.Count());
        }

        [Fact]
        public async Task CreateProduct_AddsProductAndReturnsCreatedResult()
        {
            using var context = CreateContext(nameof(CreateProduct_AddsProductAndReturnsCreatedResult));
            var controller = new ProductsController(context);
            var newProduct = new Product { Name = "USB-C Hub", Category = "Accessories", Price = 39.99m, InStock = true, CreatedDate = DateTime.UtcNow };

            var result = await controller.CreateProduct(newProduct);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdProduct = Assert.IsType<Product>(createdResult.Value);
            Assert.Equal("USB-C Hub", createdProduct.Name);
            Assert.Single(context.Products);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductMissing()
        {
            using var context = CreateContext(nameof(GetProduct_ReturnsNotFound_WhenProductMissing));
            var controller = new ProductsController(context);

            var result = await controller.GetProduct(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
