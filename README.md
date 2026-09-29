# Copilot-App-Migration-Scenarios

Sample legacy applications used to demonstrate Copilot-assisted migrations
to .NET 10. Each scenario is a small, self-contained app deliberately built
with older patterns worth modernizing.

## Copilot customization demo

The repository includes a ready-to-run [customer demo of custom agents,
skills, instructions, and prompts](docs/copilot-customization-demo.md).
It uses Scenario 4 as a repeatable ASP.NET Core 6 to .NET 10 migration story
across VS Code, Copilot CLI, and GitHub.com.

## Scenarios

| # | Scenario | Source stack | Target |
|---|---|---|---|
| 1 | [ASP.NET Web Forms + EF6 product catalog](scenarios/01-webforms-ef6-products/README.md) | .NET Framework 4.8, Web Forms, EF6, LocalDB | .NET 10 |
| 2 | [WinForms + EF6 product catalog](scenarios/02-winforms-ef6-products/README.md) | .NET Framework 4.8, WinForms, EF6, LocalDB | .NET 10 |
| 3 | [WCF SOAP service + console client](scenarios/03-wcf-products-service/README.md) | .NET Framework 4.8, WCF (IIS-hosted), EF6, LocalDB | .NET 10 |
| 4 | [ASP.NET Core 6 Web API](scenarios/04-aspnetcore6-webapi-products/README.md) | .NET 6, ASP.NET Core Web API, EF Core 6, SQLite | .NET 10 |
