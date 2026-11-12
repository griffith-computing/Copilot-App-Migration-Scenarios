# Copilot-App-Migration-Scenarios

Sample legacy applications used to demonstrate Copilot-assisted migrations
to .NET 10. Each scenario is a small, self-contained app deliberately built
with older patterns worth modernizing.

## Scenarios

| # | Scenario | Source stack | Target |
|---|---|---|---|
| 1 | [ASP.NET Web Forms + EF6 product catalog](scenarios/01-webforms-ef6-products/README.md) | .NET Framework 4.8, Web Forms, EF6, LocalDB | .NET 10 |
| 2 | [WinForms + EF6 product catalog](scenarios/02-winforms-ef6-products/README.md) | .NET Framework 4.8, WinForms, EF6, LocalDB | .NET 10 |
| 3 | [WCF SOAP service + console client](scenarios/03-wcf-products-service/README.md) | .NET Framework 4.8, WCF (IIS-hosted), EF6, LocalDB | .NET 10 |
| 4 | [ASP.NET Core 6 Web API](scenarios/04-aspnetcore6-webapi-products/README.md) | .NET 6, ASP.NET Core Web API, EF Core 6, SQLite | .NET 10 |

## Containerization agent demo

The reusable [Containerization agent](.github/agents/containerization.agent.md)
demonstrates moving an application into a container with minimal application
changes. This is separate from the .NET 10 modernization exercises above:
containerization preserves the existing framework and behavior rather than
upgrading or rewriting the application.

Select **Containerization** in a Copilot surface that supports repository custom
agents. Start with one application, for example:

```text
Assess scenarios/01-webforms-ef6-products for containerization.
Outline the necessary changes, including the Dockerfile, and wait for approval.
Keep application changes minimal and preserve the existing framework.
```

The agent first explains compatibility, prerequisites, proposed file changes,
runtime configuration, persistence, and validation commands without modifying
the repository. Review the outline and resolve any blockers, then explicitly
approve it, for example:

```text
Implement the approved outline for scenarios/01-webforms-ef6-products.
Create the Dockerfile and necessary supporting files, make only the approved
compatibility changes, and document how to build, run, and verify the container.
```

Review the resulting Dockerfile and change summary. The agent reports what it
actually verified and which checks remain blocked by missing tools or services.
If the Copilot surface cannot pause for approval, the first run returns only
the assessment; resume with the assessment and explicit approval to implement.

| Scenario | Expected assessment |
|---|---|
| Web Forms | Windows containers with IIS; LocalDB and runtime connection configuration need an approved portability solution. |
| WinForms | Interactive desktop UI is unsupported for this containerization demo; report the blocker rather than rewrite the UI. |
| WCF | Windows containers with IIS/WCF support; preserve SOAP contracts and address LocalDB connectivity. |
| ASP.NET Core API | Assess and update the existing Dockerfile, retain the current runtime, and account for SQLite persistence. |

Running the generated images requires a compatible Docker engine and any
external dependencies identified in the assessment. Windows container images
require a compatible Windows host and engine configuration. EOL runtimes such
as .NET 6 remain a documented demo risk, not a production recommendation.

To reuse the agent with another legacy application, copy
`.github\agents\containerization.agent.md` into the same location in that
repository and select it there. No application is containerized merely by
installing the agent.
