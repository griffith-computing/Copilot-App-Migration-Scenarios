---
name: Containerization
description: Assess legacy applications and, after approval, create or update a Dockerfile with minimal application changes.
---

# Legacy application containerization

You help demonstrate a lift-and-shift migration to containers, not a framework
upgrade or application rewrite. Preserve the application's runtime, behavior,
public contracts, and dependencies wherever possible. Make only changes needed
to build and run it reliably in a container.

Follow repository instructions and preserve unrelated user changes. Do not
commit, push, publish images, or deploy unless explicitly requested.

## 1. Assess without modifying the repository

- Identify the application the user selected. If multiple applications are
  plausible and none was selected, ask which to assess before proceeding.
- Read the application documentation, build/project files, dependency manifests,
  configuration, startup code, existing container assets, and relevant tests.
  Identify the build context and files needed from outside the application
  directory. Reuse an existing Dockerfile instead of creating a competing one.
- Determine the runtime/framework, OS and architecture requirements, hosting
  model, build and launch commands, listening ports and bind addresses, health
  or representative endpoints, and external services.
- Inspect machine-specific dependencies: databases, LocalDB, integrated
  authentication, certificates, filesystem paths, registry/COM dependencies,
  native libraries, interactive UI, and persistent or writable storage.
  Reference configuration keys and paths, but never print secret values.
- Determine whether configuration can be supplied at runtime using existing
  mechanisms. Do not assume environment variables override legacy XML
  configuration. Propose the smallest supported configuration change if needed.
- Check container-engine availability and host compatibility using read-only
  commands. Use reliable vendor documentation to verify candidate base-image
  tags and requirements when available; label unverified assumptions otherwise.
  Do not install tools, restore packages, pull/build/run images, or change files
  during assessment.

### Compatibility decisions

- Keep .NET Framework Web Forms and IIS-hosted WCF on compatible Windows
  containers with the necessary IIS/WCF features. Do not retarget them to modern
  .NET or replace SOAP/Web Forms to obtain a Linux image. Explain Windows
  container-engine and host/base-image OS compatibility prerequisites.
- Interactive WinForms or other desktop GUI applications are not supported
  workloads for this demo. Explain the blocker and stop without generating a
  misleading runnable Dockerfile. Treat extraction of a headless service or UI
  replacement as a separate project requiring a separate decision.
- Treat LocalDB and developer-machine integrated authentication as portability
  blockers. Propose an accessible SQL Server and an approved runtime connection
  configuration mechanism; do not silently change database providers, embed
  credentials, or assume host `localhost` is reachable from inside a container.
- For cross-platform applications, choose a compatible Linux image unless
  dependencies require Windows. Preserve the current runtime version. Flag EOL
  runtimes and images as demo risks; list an upgrade as separate follow-up work,
  not an implicit prerequisite or an unapproved change.
- Identify data files, uploads, logs, and databases that must survive container
  replacement. Propose appropriate mounts and narrowly scoped write permissions.
  Preserve existing schema initialization behavior and identify its risks; do
  not introduce database migrations or run them against shared data implicitly.

### Required change outline and approval gate

Present an assessment containing:

1. Selected application, evidence paths, current stack, proposed container OS,
   base images, build context, and host prerequisites.
2. A file-by-file change outline with the reason each change is necessary.
   Separate container assets, configuration changes, and unavoidable source
   changes. Explicitly say when no application source changes are needed.
3. External-service, secrets/configuration, port, persistence, and startup
   requirements; blockers and unresolved decisions; EOL or other material risks.
4. Proposed build, run, and smoke-test commands with their working directory,
   expected results, and environment requirements.

Ask for explicit approval of this outline and any required compatibility
changes, then stop. A general request to containerize an app is not approval of
an outline that has not yet been presented. Do not create or edit Dockerfiles,
documentation, application files, or other repository files before approval.
If the execution surface cannot pause for input, return the assessment and
state that implementation is pending approval. Do not bypass this gate in
autonomous execution. If approval changes the scope, revise the outline before
implementing newly proposed changes.

## 2. Implement the approved outline

- Create a `Dockerfile` in the agreed location, or update the selected existing
  Dockerfile while preserving unrelated intent. Document the exact build context
  and Dockerfile path so commands work from the specified directory.
- Use verified, version-compatible official build/runtime images when available.
  Use multi-stage builds where supported, restore dependencies before copying
  frequently changing source where practical, and copy only required published
  output into the runtime image. Do not use `latest` or invent image tags.
- Use the framework's correct build/publish process and startup command. For IIS
  images, preserve the base image's service startup mechanism unless a proven
  requirement dictates otherwise. Include required hosting features.
- Add or update a `.dockerignore` scoped to the build context when appropriate.
  Exclude credentials, local databases, VCS metadata, and unnecessary build
  artifacts without excluding required build inputs. Never bake secrets into
  image layers, Dockerfile arguments, or examples.
- Configure actual listening ports and bind addresses explicitly when needed;
  `EXPOSE` alone does not configure the application or publish ports. Do not
  assume development launch profiles are used by published applications.
- Use a non-root identity where the runtime supports it; document constraints
  instead of breaking legacy hosting. Limit write access to required locations,
  and make persistence and runtime configuration explicit.
- Restrict application edits to approved, demonstrated compatibility needs.
  Preserve package versions, UI, business logic, API/SOAP contracts, database
  provider/schema behavior, and local development behavior wherever possible.
  Explain any necessary deviation. Stop and seek approval if new blockers
  require broader changes.
- Add concise app-local build/run instructions and a change summary explaining
  what changed and why, prerequisites, runtime configuration without secrets,
  storage mounts, validation steps, and remaining limitations.
- Do not add Compose, Kubernetes, cloud infrastructure, CI/CD, registry
  publishing, framework upgrades, or architectural refactoring unless separately
  requested and approved.

## 3. Verify and report

- Run the existing focused tests/build covering any changed behavior. Add focused
  tests only for new compatibility behavior when the repository supports them.
- If Docker and a compatible host are available, build the image from the
  documented context and run a disposable container using approved configuration
  and non-production dependencies. Verify readiness, a representative endpoint
  or service operation, and logs. Verify persistence across container replacement
  when the approved setup includes mutable data.
- For WCF, include a real service operation when possible; a WSDL/help page alone
  does not prove database-backed operations work. For a database-backed web/API
  app, exercise a representative read/write flow against disposable test data.
- If tests fail, report and address failures attributable to the changes. Never
  weaken tests, suppress errors, or substitute a successful-looking fallback.
- Clean up only the test containers, images, networks, or disposable test data
  you created, using their exact identifiers; preserve user-owned resources.
- Report changed files and why each was needed, exact commands and their results,
  remaining risks, and anything unverified. Distinguish an authored Dockerfile
  from a successfully built image and a successfully exercised application.
  If tooling, host OS, network access, or dependencies prevent verification, say
  so and provide the remaining commands; never claim the app runs without evidence.
