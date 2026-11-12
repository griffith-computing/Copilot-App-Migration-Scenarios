# .NET upgrade checklist

Use the applicable items; do not force optional modernization into a framework-only upgrade.

## Version-coupled surfaces

- Application and test `TargetFramework` values
- `global.json` SDK version and roll-forward policy
- Framework-family NuGet packages and tools
- Test SDK, test framework, and coverage packages
- Build and runtime Docker image tags
- CI matrices, setup actions, scripts, and deployment manifests

## Compatibility decisions

- Hosting and application startup model
- Middleware order and environment checks
- JSON serializer and wire-format behavior
- Entity Framework provider versions and initialization strategy
- Configuration keys, connection strings, and secrets handling
- Removed, obsolete, or behavior-changing APIs

## Validation

- Restore succeeds without downgrade or incompatible-package warnings
- Application and tests build for the target framework
- Existing tests pass without weakening assertions
- Container builds when Docker-related files changed and Docker is available
- Documentation and examples name the new runtime and commands
- No unrelated project, generated output, database, or secret is included
