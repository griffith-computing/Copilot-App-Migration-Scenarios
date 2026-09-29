---
name: Scenario 4 .NET migration
description: Guidance for upgrading the Scenario 4 Products API and its tests from ASP.NET Core 6 to .NET 10.
applyTo: "scenarios/04-aspnetcore6-webapi-products/**"
---

# Scenario 4 migration requirements

- Target .NET 10 across the API project, test project, `global.json`, package references, and both Dockerfile stages.
- Keep the existing controller routes, status codes, JSON field names, SQLite connection, seed data, and test intent unless the request changes them.
- Prefer the current ASP.NET Core minimal hosting model in `Program.cs`; remove `Startup.cs` only after all service registration, middleware ordering, database initialization, and Swagger behavior have been preserved.
- Prefer the built-in `System.Text.Json` serializer. Before removing Newtonsoft.Json, verify that the default ASP.NET Core web serializer preserves the current camel-case JSON contract.
- Keep EF Core provider packages on the same compatible major version. Do not combine `EnsureCreated` and EF Core migrations. Introduce migrations only when persistence modernization is in scope.
- Update test dependencies deliberately. Do not change assertions merely to make an upgrade pass.
- Keep package versions explicit and avoid prerelease packages unless the request requires them.
- Run the Scenario 4 test project after changes. If Docker is available and the Dockerfile changed, build the image as a separate validation step.
- Update `scenarios/04-aspnetcore6-webapi-products/README.md` so its framework description, prerequisites, commands, and migration-target table match the resulting code.
