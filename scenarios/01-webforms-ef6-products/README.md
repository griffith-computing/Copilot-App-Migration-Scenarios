# Scenario 1: ASP.NET Web Forms + EF6 → .NET 10 migration candidate

`ProductsApp` is a small, intentionally "legacy" ASP.NET Web Forms
application targeting **.NET Framework 4.8**. It exists purely as a
migration demo candidate: a realistic-but-small app that a Copilot-assisted
modernization pass can be run against, targeting **.NET 10**.

## What the app does

A simple product catalog:

- `Default.aspx` — lists all products in a `GridView`, links to detail pages.
- `ProductDetail.aspx` — shows a single product (`?id=`).
- `ProductCreate.aspx` — a form to add a new product, with Web Forms
  validator controls (`RequiredFieldValidator`, `RangeValidator`).
- `Site.Master` — shared page chrome (header/nav/footer).

Data is stored via **EF6 Code-First** (`ProductsApp.Models.ProductsContext`)
against **SQL Server LocalDB**, with a Code-First Migration
(`Migrations/`) that creates the `Products` table and seeds sample rows.

## Intentional legacy patterns (migration targets)

This app deliberately includes patterns that don't translate 1:1 to
ASP.NET Core / .NET 10, so a migration exercise has real work to do:

| Pattern | Where | Why it matters for migration |
|---|---|---|
| Web Forms page lifecycle & ViewState | `*.aspx` / code-behind | No equivalent in ASP.NET Core; pages become Razor Pages/MVC/Blazor views. |
| `Web.config` `system.web` pipeline | `Web.config` | Replaced by `Program.cs`/middleware configuration in .NET 10. |
| Web.config transforms (`Web.Debug.config` / `Web.Release.config`) | `Web.*.config` | Replaced by `appsettings.{Environment}.json` + environment variables. |
| Custom `IHttpModule` registered in config | `RequestAuditModule.cs`, `Web.config` | Becomes ASP.NET Core middleware, registered in code, not config. |
| `Global.asax` application/session events | `Global.asax(.cs)` | Replaced by `Program.cs` startup code and DI-scoped services. |
| EF6 Code-First + Migrations, `DbContext` resolved via `new` per request | `Models/`, `Migrations/` | Migrates to EF Core; DbContext should move to DI (`AddDbContext`) instead of manual `using`. |
| `packages.config` + non-SDK `.csproj` | `ProductsApp.csproj`, `packages.config` | Migrates to SDK-style `.csproj` + `PackageReference`. |
| `ConfigurationManager.AppSettings` / `connectionStrings` | `Web.config`, code-behind | Replaced by `IConfiguration` / `appsettings.json`. |
| Forms authentication | `Web.config` (`<authentication mode="Forms">`) | Replaced by ASP.NET Core Identity / cookie or token auth. |

## Running locally (Windows + Visual Studio)

1. Requires Visual Studio 2019+ with the **ASP.NET and web development**
   workload, and **SQL Server Express LocalDB** (installed by default with
   that workload).
2. Open `ProductsApp.sln`, restore NuGet packages (`EntityFramework` 6.4.4).
3. Set `ProductsApp` as the startup project and run (F5). IIS Express will
   host it; EF6 will auto-create/migrate the LocalDB database and seed
   sample products on first request (see `Global.asax.cs` /
   `Migrations/Configuration.cs`).

## Not included (by design, scenario is kept small)

Editing/deleting products, authentication enforcement, and automated tests
are intentionally left out to keep this first scenario focused. Later
scenarios in this repo will layer in more complexity (WCF services, Windows
auth, background jobs, etc.).
