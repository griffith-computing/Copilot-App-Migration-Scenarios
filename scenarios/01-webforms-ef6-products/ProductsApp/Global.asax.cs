using System;
using System.Data.Entity;
using System.Web;
using ProductsApp.Migrations;
using ProductsApp.Models;

namespace ProductsApp
{
    /// <summary>
    /// Application-wide event handlers. In a Web Forms app these fire from
    /// the ASP.NET runtime's global.asax pipeline (Application_Start,
    /// Session_Start, Application_Error, ...). ASP.NET Core/.NET 10 replaces
    /// this with explicit startup code (Program.cs) and middleware, so this
    /// file is one of the first things a migration has to re-home.
    /// </summary>
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Legacy Code-First Migrations bootstrapping: runs pending EF6
            // migrations automatically on app start.
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProductsContext, Configuration>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Placeholder for legacy per-session state initialization.
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            System.Diagnostics.Trace.WriteLine("[Global] Unhandled exception: " + exception);
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
