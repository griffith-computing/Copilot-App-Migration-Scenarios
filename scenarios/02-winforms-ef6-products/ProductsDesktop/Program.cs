using System;
using System.Data.Entity;
using System.Windows.Forms;
using ProductsDesktop.Migrations;
using ProductsDesktop.Models;

namespace ProductsDesktop
{
    /// <summary>
    /// Classic WinForms entry point: a [STAThread] Main that manually wires
    /// up visual styles and runs the message loop via Application.Run.
    /// Modern .NET desktop apps typically wrap this in a generic host
    /// (Host.CreateApplicationBuilder) for DI/config/logging -- this app
    /// intentionally does none of that, so a migration has a real bootstrap
    /// to modernize.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Legacy Code-First Migrations bootstrapping: runs pending EF6
            // migrations automatically on app start.
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProductsContext, Configuration>());

            Application.Run(new MainForm());
        }
    }
}
