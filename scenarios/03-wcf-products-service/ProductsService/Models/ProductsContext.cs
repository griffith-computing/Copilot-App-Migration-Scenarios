using System.Data.Entity;

namespace ProductsService.Models
{
    /// <summary>
    /// EF6 DbContext. Connection string name matches the entry in
    /// Web.config's &lt;connectionStrings&gt; section.
    /// </summary>
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
