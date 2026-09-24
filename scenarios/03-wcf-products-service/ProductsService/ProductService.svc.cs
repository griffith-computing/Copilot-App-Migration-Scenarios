using System.Collections.Generic;
using System.Linq;
using ProductsService.Models;

namespace ProductsService
{
    /// <summary>
    /// Service implementation, hosted by IIS via ProductService.svc's
    /// ServiceHost factory model. Each operation opens its own EF6
    /// DbContext -- WCF's per-call instancing (the default) means there is
    /// no natural request-scoped DI container the way ASP.NET Core
    /// provides, so this "new DbContext per call" pattern is common in
    /// legacy WCF services.
    /// </summary>
    public class ProductService : IProductService
    {
        public List<ProductDto> GetProducts()
        {
            using (var db = new ProductsContext())
            {
                return db.Products
                    .OrderBy(p => p.Name)
                    .Select(ToDto)
                    .ToList();
            }
        }

        public ProductDto GetProduct(int id)
        {
            using (var db = new ProductsContext())
            {
                var product = db.Products.Find(id);
                return product == null ? null : ToDto(product);
            }
        }

        public ProductDto CreateProduct(ProductDto product)
        {
            using (var db = new ProductsContext())
            {
                var entity = new Product
                {
                    Name = product.Name,
                    Category = product.Category,
                    Price = product.Price,
                    InStock = product.InStock,
                    CreatedDate = product.CreatedDate
                };

                db.Products.Add(entity);
                db.SaveChanges();

                return ToDto(entity);
            }
        }

        public ProductDto UpdateProduct(ProductDto product)
        {
            using (var db = new ProductsContext())
            {
                var entity = db.Products.Find(product.Id);
                if (entity == null)
                {
                    return null;
                }

                entity.Name = product.Name;
                entity.Category = product.Category;
                entity.Price = product.Price;
                entity.InStock = product.InStock;

                db.SaveChanges();

                return ToDto(entity);
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var db = new ProductsContext())
            {
                var entity = db.Products.Find(id);
                if (entity == null)
                {
                    return false;
                }

                db.Products.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        private static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                InStock = product.InStock,
                CreatedDate = product.CreatedDate
            };
        }
    }
}
