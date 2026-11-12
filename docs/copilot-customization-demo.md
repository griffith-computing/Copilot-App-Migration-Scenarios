# Copilot customization customer demo

This demo uses the intentionally legacy [Scenario 4 Products API](../scenarios/04-aspnetcore6-webapi-products/README.md) to show how four Copilot customization types cooperate during an ASP.NET Core 6 to .NET 10 migration.

The repository keeps Scenario 4 on .NET 6. The migration is performed live so each session starts from the same meaningful baseline.

## What each customization demonstrates

| Customization | File | Role in the story |
|---|---|---|
| Repository instructions | [`.github/copilot-instructions.md`](../.github/copilot-instructions.md) | Always-on repository context and safety boundaries |
| Path-specific instructions | [`.github/instructions/dotnet-migration.instructions.md`](../.github/instructions/dotnet-migration.instructions.md) | Requirements automatically attached when Scenario 4 files are changed |
| Custom agent | [`.github/agents/dotnet-migration.agent.md`](../.github/agents/dotnet-migration.agent.md) | A migration specialist with a defined role, tools, workflow, and output |
| Agent skill | [`.github/skills/dotnet-upgrade/SKILL.md`](../.github/skills/dotnet-upgrade/SKILL.md) | A portable, on-demand upgrade procedure with a reusable checklist |
| Prompt file | [`.github/prompts/migrate-products-api.prompt.md`](../.github/prompts/migrate-products-api.prompt.md) | A repeatable VS Code slash-command entry point for this exact demo |

The separation is deliberate: instructions define rules, the agent defines who performs the work, the skill teaches a reusable procedure, and the prompt supplies the specific task.

## Surface support

| Customization | VS Code Copilot | Copilot CLI | GitHub.com cloud agent |
|---|---:|---:|---:|
| `.github/copilot-instructions.md` | Yes | Yes | Yes |
| `.github/instructions/*.instructions.md` | Yes | Yes | Yes |
| `.github/agents/*.agent.md` | Yes | Yes | Yes |
| `.github/skills/*/SKILL.md` | Yes | Yes | Yes |
| `.github/prompts/*.prompt.md` | Local agent only | No | No |

Prompt files are included because they make the slash-command concept easy to demonstrate in VS Code. They are deprecated for Agent Host sessions and are not loaded by Copilot CLI or GitHub.com. Use the `dotnet-upgrade` skill as the portable replacement for reusable task workflows.

## Preflight

1. Use a clean branch or disposable worktree so the live migration can be discarded afterward.
2. Confirm the repository customization files are visible in the client.
3. Confirm a .NET SDK capable of building the baseline is installed:

   ```powershell
   dotnet --info
   dotnet test scenarios/04-aspnetcore6-webapi-products/ProductsApi.Tests/ProductsApi.Tests.csproj
   ```

4. If the full-scope demo will build the container, confirm Docker is running. Docker is not required for the framework-only flow.
5. Open the Scenario 4 README, `ProductsApi.csproj`, `Startup.cs`, and the test project before starting. These make the before-and-after changes easy to explain.

## Recommended live flow

### 1. Show automatic instructions

Open `ProductsApi/Startup.cs` and ask:

> What constraints should you follow if I ask you to migrate this application?

Point out that Copilot can describe the intentional baseline, scenario boundary, behavior-preservation requirements, .NET 10 target, and validation command without those details being repeated in the question.

### 2. Show the custom agent

Select **.NET Migration** from the agent picker and ask:

> Assess Scenario 4 for a framework-only migration to .NET 10. Do not edit files yet.

Highlight the agent's specialized inventory: project targets, `global.json`, NuGet versions, startup model, Newtonsoft.Json, `EnsureCreated`, Docker images, tests, and documentation.

In Copilot CLI or GitHub.com, select the same repository agent and use the same request. This is the best point to show that the agent definition travels with the repository.

### 3. Show the skill

Invoke `/dotnet-upgrade` if the client exposes skills as slash commands, or ask:

> Use the dotnet-upgrade skill to explain the order in which you would migrate Scenario 4 to .NET 10.

Open `upgrade-checklist.md` to show that a skill can package supporting resources rather than placing every detail in one always-on instruction file.

### 4. Run the end-to-end migration

For a predictable, shorter customer demo, use `scope=framework-only`.

In VS Code with the Local agent:

```text
/migrate-products-api scope=framework-only
```

With the **.NET Migration** agent selected in VS Code Agent Host, Copilot CLI, or GitHub.com:

> Migrate Scenario 4 from ASP.NET Core 6 to .NET 10. Use the dotnet-upgrade skill and keep this a framework-only migration. Implement and validate the changes.

The expected result updates both project targets, SDK selection, packages, minimal hosting, serialization, Docker images, tests as needed, and the Scenario 4 README. It should explicitly defer replacing `EnsureCreated`.

Use `scope=full` only when there is enough time to discuss EF Core migrations and validate the additional persistence change.

## Expected talking points

- **Less prompt repetition:** stable repository facts and guardrails are automatic.
- **Narrow context:** Scenario 4 guidance activates only for the relevant path.
- **Role specialization:** the migration agent has a bounded toolset and repeatable engineering workflow.
- **Progressive disclosure:** the skill and checklist load for relevant upgrade work instead of consuming every conversation's context.
- **Repeatability:** the prompt file turns the complete VS Code demo request into a named command.
- **Portability:** repository agents and skills work across the modern Copilot surfaces; the support table makes the prompt-file boundary explicit.
- **Governance:** every layer reinforces scenario isolation, behavior preservation, and validation rather than relying on presenter memory.

## Reset for another run

The safest reset is to discard the disposable demo branch or worktree and create a fresh one from the commit containing these customization files. This removes generated EF migrations, databases, or other files that a full-scope run might create without relying on broad cleanup commands.

Before a customer session, rehearse from a new worktree using the same Copilot surface, model availability, network access, and demo scope planned for the presentation.
