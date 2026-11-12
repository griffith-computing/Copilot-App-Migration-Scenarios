---
name: migrate-products-api
description: Run the customer demo that migrates the Scenario 4 Products API from .NET 6 to .NET 10.
argument-hint: "scope=framework-only|full"
agent: ".NET Migration"
---

Migrate [Scenario 4](../../scenarios/04-aspnetcore6-webapi-products/README.md) from ASP.NET Core 6 to .NET 10.

Use the repository instructions, the Scenario 4 path-specific instructions, and the `dotnet-upgrade` skill. The requested scope is `${input:scope:framework-only}`.

Required outcomes:

- Upgrade the API and test projects, SDK selection, compatible packages, and both Dockerfile stages to .NET 10.
- Replace the `Startup` hosting pattern with minimal hosting in `Program.cs` while preserving registrations, middleware behavior, Swagger behavior, SQLite setup, and seed data.
- Remove the Newtonsoft.Json dependency and attributes only after confirming that `System.Text.Json` preserves the existing camel-case response contract.
- Preserve controller routes, status codes, model shape, seed data, and test intent.
- Update the Scenario 4 README and run its existing test project.
- Do not edit Scenarios 1-3.

If `scope=full`, also replace `EnsureCreated` with a proper EF Core migration and add or update focused tests for any behavior affected by that change. If the scope is `framework-only`, explicitly defer persistence modernization.

Finish with a concise migration summary, validation result, and deferred optional work.
