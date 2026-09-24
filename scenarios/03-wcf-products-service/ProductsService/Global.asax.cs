using System.Data.Entity;
using System.Web;
using ProductsService.Migrations;
using ProductsService.Models;

namespace ProductsService
{
    /// <summary>
    /// Minimal Global.asax, present solely to bootstrap the EF6 Code-First
    /// Migration on application start -- the WCF service itself has no
    /// other use for the ASP.NET application lifecycle.
    /// </summary>
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProductsContext, Configuration>());
        }
    }
}
