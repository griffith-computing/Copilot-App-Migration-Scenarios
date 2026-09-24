# Scenario 4: ASP.NET Core 6 Web API -> .NET 10 migration candidate

`ProductsApi` is a small ASP.NET Core **6.0** Web API (with an accompanying
xUnit test project) demonstrating a different flavor of migration than
scenarios 1-3: it's **already on modern, cross-platform .NET** rather than
.NET Framework, so the work is a *version upgrade* (retarget, bump
packages, fix breaking changes) instead of a full architectural rewrite.
This is by far the most common real-world .NET migration: .NET 6 went out
of support in November 2024, and a huge number of production services are
still sitting on it.

## What the app does

The same Products domain as the other scenarios, exposed as a REST API:

- **`ProductsApi`** - `ProductsController` with full CRUD (`GET`/`POST`/
  `PUT`/`DELETE`) over `/api/products`, backed by EF Core 6 + SQLite.
  Swagger/OpenAPI docs available at `/swagger` in Development.
- **`ProductsApi.Tests`** - xUnit tests against the controller using EF
  Core's InMemory provider, covering list/create/not-found behavior.

## Intentional legacy patterns (migration targets)

Unlike the .NET Framework scenarios, none of these are hard blockers --
they're all still fully supported on .NET 6. That's the point: this
scenario is about the *lower-friction but still real* work involved in
catching a service up to the latest LTS.

| Pattern | Where | Why it matters for migration |
|---|---|---|
| `Startup.cs` / `ConfigureServices`+`Configure` split | `Startup.cs`, `Program.cs` | Still works on .NET 6, but real .NET 6 repos upgraded from .NET 5 rarely adopted the newer top-level-statement "minimal hosting" model. A .NET 10 migration is a natural point to collapse this into one file. |
| Pinned, un-upgraded NuGet packages (`EntityFramework Core 6.0.36`, `Swashbuckle.AspNetCore 6.4.0`) | `ProductsApi.csproj` | Gives a migration real package-compatibility work to do (checking for `net6.0`-only APIs, deprecated members, major-version behavior changes) rather than just a `TargetFramework` string edit. |
| `Microsoft.AspNetCore.Mvc.NewtonsoftJson` + `JsonProperty` attributes | `Product.cs`, `Startup.cs` | Carried-forward Json.NET dependency from an earlier codebase. Migration should ask: still needed, or can it finally move to `System.Text.Json`? |
| `global.json` pinning an exact SDK feature band | `global.json` | Explicit SDK pins are a common, easy-to-miss blocker -- CI/dev machines can't build with a newer SDK until this is bumped too. *(Set to `rollForward: latestMajor` here only so this sample stays buildable in an environment that ships just the latest SDK; a real out-of-date repo would typically omit `rollForward` or set it much stricter.)* |
| `EnsureCreated()` instead of EF Core Migrations | `ProductsDbContext.cs` | Common prototype/small-service shortcut. Worth replacing with real migrations (or idempotent scripts) as part of modernizing. |
| Dockerfile pinned to `mcr.microsoft.com/dotnet/sdk:6.0` / `aspnet:6.0` | `Dockerfile` | Base image tags need bumping in lockstep with the TFM, and it's easy to update one without the other. |

## Running locally

Requires the .NET SDK (6.0, or a newer SDK with `global.json` roll-forward
as configured here).

```powershell
cd scenarios/04-aspnetcore6-webapi-products
dotnet test ProductsApi.Tests/ProductsApi.Tests.csproj
dotnet run --project ProductsApi/ProductsApi.csproj
# then browse http://localhost:5223/swagger (see launchSettings.json)
```

The API uses a local SQLite file (`products.db`, gitignored) seeded with
three sample products on first run.

## Not included (by design, scenario is kept small)

Authentication, API versioning, and rate limiting are intentionally left
out to keep the scenario focused on the package/hosting-model/config
upgrade story above.
