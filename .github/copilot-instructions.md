# Repository instructions

This repository contains small applications that are intentionally kept on older .NET stacks so they can be used for migration demonstrations. Treat the current framework versions, packages, hosting patterns, and compatibility shims as deliberate baselines, not incidental technical debt.

## Scope

- Change only the scenario named in the request. The scenarios are independent applications that happen to share a Products domain.
- Do not modernize a scenario unless the request explicitly asks for a migration or upgrade.
- Preserve externally visible behavior during migrations unless a behavior change is explicitly requested.
- Prefer focused changes over introducing new architecture, packages, or infrastructure.
- Never add credentials, generated databases, build output, or user-specific configuration to the repository.

## Working with the scenarios

- Read the selected scenario's `README.md` before editing it. It documents intentional migration targets and exclusions.
- Inspect the project and configuration files before choosing an upgrade path. Keep the target framework, SDK selection, NuGet packages, tests, and container images consistent.
- Use existing conventions in the selected scenario. Add comments only when they explain a non-obvious compatibility or migration decision.
- Update the scenario documentation when its prerequisites, commands, framework target, or intentional migration targets change.

## Validation

- Run the smallest build or test command that covers the changed scenario.
- For Scenario 4, run:

  ```powershell
  dotnet test scenarios/04-aspnetcore6-webapi-products/ProductsApi.Tests/ProductsApi.Tests.csproj
  ```

- Treat warnings caused by a migration, package downgrade, or obsolete API as work to investigate rather than suppress.
- Report what changed, any compatibility decisions, and the validation result.
