---
name: dotnet-upgrade
description: Upgrade a .NET application to a newer supported release. Use for target-framework migrations, ASP.NET Core hosting modernization, coordinated NuGet and Docker updates, or diagnosing build and test failures caused by a .NET version change.
argument-hint: "[project or solution path] [target .NET version]"
---

# .NET upgrade workflow

Use this skill when a request changes a .NET target framework or modernizes an application as part of that change. Follow the [upgrade checklist](./upgrade-checklist.md) and keep a short record of decisions that affect compatibility.

## Procedure

1. **Establish the baseline**
   - Read repository instructions and the selected application's documentation.
   - Locate all project files, solution files, `global.json`, central package files, Dockerfiles, tests, and CI definitions.
   - Run the smallest existing test command before editing when the environment supports the current runtime.

2. **Define the required change**
   - Separate framework-required work from optional modernization.
   - Preserve public APIs, serialized contracts, configuration keys, and persistence behavior by default.
   - Check official compatibility and package information when an API or package choice is uncertain; do not guess version numbers.

3. **Upgrade coherently**
   - Update application and test target frameworks together.
   - Align SDK selection, framework-specific packages, analyzers, tools, and container images with the target release.
   - Remove compatibility packages only after verifying replacement behavior.
   - Modernize hosting or startup code without changing service registration, middleware ordering, or environment-specific behavior.

4. **Validate in layers**
   - Restore and build first when that gives clearer diagnostics, then run the focused test project.
   - Treat package downgrade, vulnerability, obsolete API, and target-framework warnings as actionable.
   - If a Dockerfile changed and Docker is available, build the image after the .NET tests pass.
   - Search changed documentation and configuration for stale version references.

5. **Report**
   - Summarize old and new framework/package versions.
   - Explain serializer, hosting, persistence, and compatibility decisions.
   - State the commands run and identify optional work that was intentionally deferred.

Do not broaden the task to unrelated projects or suppress upgrade warnings to produce a green build.
