# Scenario 2: WinForms + EF6 → .NET 10 migration candidate

`ProductsDesktop` is a small, intentionally "legacy" WinForms application
targeting **.NET Framework 4.8**. It exists purely as a migration demo
candidate: a realistic-but-small desktop app that a Copilot-assisted
modernization pass can be run against, targeting **.NET 10** (WinForms is
a first-class, supported UI framework on modern .NET, so this is a
straightforward-but-real migration story, distinct from scenario 1's Web
Forms app which has no direct successor).

## What the app does

The same product catalog domain as [scenario 1](../01-webforms-ef6-products/README.md),
as a desktop app instead of a web app:

- `MainForm` — a `DataGridView` listing all products, with a menu/toolbar
  (Refresh, Add, Edit, Delete) and a status strip showing the row count.
  Double-clicking a row opens it for editing.
- `ProductEditForm` — a modal dialog (`ShowDialog`) for adding or editing
  a product, using the classic `ErrorProvider` validation pattern.

Data is stored via **EF6 Code-First** (`ProductsDesktop.Models.ProductsContext`)
against **SQL Server LocalDB**, with a Code-First Migration
(`Migrations/`) that creates the `Products` table and seeds sample rows —
this project has its own copy of the model/context (not shared with
scenario 1) so each scenario stays independently runnable.

## Intentional legacy patterns (migration targets)

| Pattern | Where | Why it matters for migration |
|---|---|---|
| `[STAThread]` `Main` + `Application.Run` bootstrap | `Program.cs` | Modern .NET desktop apps often wrap this in a generic host (`Host.CreateApplicationBuilder`) for DI/config/logging. |
| Designer-generated partial classes + `.resx` per form | `MainForm.Designer.cs`, `ProductEditForm.Designer.cs`, `*.resx` | Still supported in .NET, but needs careful handling (regen, WinForms Designer support) when the project is retargeted. |
| `App.config` (`connectionStrings`, `appSettings`, `entityFramework`, binding redirects) | `App.config` | Replaced by `appsettings.json` / `IConfiguration` in modern .NET, and EF6 config sections don't apply to EF Core. |
| `packages.config` + non-SDK `.csproj` | `ProductsDesktop.csproj`, `packages.config` | Migrates to SDK-style `.csproj` (`Sdk="Microsoft.NET.Sdk"` with `UseWindowsForms`) + `PackageReference`. |
| EF6 Code-First + Migrations, `DbContext` created via `new` per operation | `Models/`, `Migrations/` | Migrates to EF Core; `DbContext` should move to DI instead of manual `using`. |
| Synchronous, UI-thread-blocking data access in click handlers | `MainForm.cs`, `ProductEditForm.cs` | Modern WinForms/.NET code should use `async`/`await` to keep the UI responsive. |
| `ErrorProvider`-based validation | `ProductEditForm.cs` | Still available on .NET, but a good example of a UI validation pattern to review/carry forward deliberately rather than by accident. |

## Running locally (Windows + Visual Studio)

1. Requires Visual Studio 2019+ with the **.NET desktop development**
   workload, and **SQL Server Express LocalDB** (installed by default with
   that workload).
2. Open `ProductsDesktop.sln`, restore NuGet packages (`EntityFramework`
   6.4.4).
3. Set `ProductsDesktop` as the startup project and run (F5). EF6 will
   auto-create/migrate the LocalDB database and seed sample products on
   first run (see `Program.cs` / `Migrations/Configuration.cs`).

## Not included (by design, scenario is kept small)

Sorting/filtering the grid, multi-select delete, and automated UI tests
are intentionally left out to keep this scenario focused on the core
migration-relevant patterns above.
