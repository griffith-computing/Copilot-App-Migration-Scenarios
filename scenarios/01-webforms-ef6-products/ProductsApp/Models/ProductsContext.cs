using System.Data.Entity;

namespace ProductsApp.Models
{
    /// <summary>
    /// EF6 DbContext. Connection string name matches the entry in
    /// Web.config's &lt;connectionStrings&gt; section (legacy EF6 config
    /// convention; EF Core in .NET 10 configures this in code/DI instead).
    /// </summary>
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
