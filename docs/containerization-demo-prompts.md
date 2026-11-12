# Containerization demo prompts for Solution Engineers

Use these prompts with the [Containerization custom agent](../.github/agents/containerization.agent.md)
to demonstrate containerizing the [sample applications](../README.md#scenarios)
with minimal application changes. Copy one prompt at a time, review the actual
response, and resolve blockers before moving to the next stage.

This is a lift-and-shift demo, not the repository's .NET 10 modernization
exercise. Keep existing frameworks, packages, application behavior, and service
contracts. EOL runtimes such as .NET 6 are demo risks, not production
recommendations. These prompts do not authorize deployment, image publishing,
commits, infrastructure provisioning, or unrelated modernization.

## Presenter setup and flow

Open the repository in a Copilot surface that supports repository custom agents
and select **Containerization**. Start a separate conversation for each sample
to avoid carrying approval from one application into another. Prompts below use
repository-relative Windows paths; request PowerShell commands from the agent.

The sequence is **assess -> review the outline -> explicitly approve and
implement -> verify -> explain results**. Assessment is read-only. Do not send
an approval prompt until you have reviewed the generated file-by-file outline
and resolved its decisions. Approval applies only to that outline, not to
unanticipated changes. If the surface cannot pause, keep its returned assessment
and supply it with approval in a subsequent run.

Begin with this shared preflight prompt:

```text
Prepare a read-only containerization demo preflight for this repository.
Check the host OS, available build tools, Docker CLI and engine availability,
engine OS mode, and relevant Windows host/image compatibility constraints.
Do not install tools, restore packages, pull images, switch engine modes, change
files, or start containers. Give me the prerequisites and missing capabilities
for the four samples, distinguishing assessment-only demos from runnable demos.
Use PowerShell for any commands and state their working directory.
```

**Checkpoint:** Web Forms and WCF need compatible Windows container hosting.
The API can use Linux containers. Do not assume one engine mode supports both
at once or switch it while other workloads are running. WinForms is the
unsupported-workload demonstration. SQL Server for the web/service demos must
be accessible from the container; LocalDB is not that service.

For runnable demos, use disposable, non-production data. Supply environment
details through your approved local configuration process, not passwords in
chat, source files, images, or command examples. If prerequisites are missing,
use the troubleshooting prompts instead of pretending execution succeeded.

## Scenario 1: Web Forms with EF6

### Assess

```text
Assess scenarios\01-webforms-ef6-products for containerization without changing
files or running setup. Preserve .NET Framework 4.8, Web Forms, EF6, and the UI.
Outline the Dockerfile and each necessary supporting or application change,
with evidence paths and reasons. Identify compatible Windows/IIS hosting,
build context, runtime configuration, ports, and host requirements.
Explain why LocalDB and its current integrated-authentication connection are
blockers. Identify the existing database initialization behavior and its risks.
Propose verification commands and expected results, then stop for approval.
```

**Checkpoint:** Expect a Windows/IIS proposal, not a Linux image or an ASP.NET
Core rewrite. A Dockerfile alone does not solve SQL connectivity.

### Review the dependency decision

```text
Review the proposed Web Forms outline with me before implementation.
Describe the non-secret details needed for an accessible, dedicated demo SQL
Server database and the smallest runtime connection-configuration mechanism.
Do not assume environment variables override Web.config automatically, that
container localhost is the host, or that integrated authentication will work.
Keep EF6 and the database provider. Do not provision infrastructure or request
passwords. Identify required decisions and revise the file-by-file outline
as needed; remain read-only and stop for approval.
```

**Checkpoint:** Arrange the approved external database and secure configuration
outside this walkthrough. Review any source/configuration edits needed to
replace machine-specific assumptions. Do not proceed while these are unresolved.

### Approve and implement

Use only after accepting the actual outline and resolving database prerequisites:

```text
I approve the latest reviewed outline for scenarios\01-webforms-ef6-products,
including its agreed runtime connection-configuration approach. Implement only
those changes. Create the Dockerfile and scoped supporting assets, preserve
the framework, EF6, and UI behavior, and document exact PowerShell build/run
commands, build context, ports, and database prerequisites without secrets.
Stop for revised approval if new blockers require changes outside the outline.
```

**Checkpoint:** Review the diff: application edits must each have a
container-compatibility reason. Database provisioning and framework upgrades
are not part of this approval.

### Verify

```text
Verify the approved Web Forms container setup using compatible Docker hosting
and the configured disposable demo database, if available.
Build and run it using the documented context. Check Default.aspx,
ProductCreate.aspx, and ProductDetail.aspx: list products, create a uniquely
named demo product through the form, and open its details. Check logs and
confirm the product remains after replacing only the demo container.
Never use shared or production data. Report exact commands, observed results,
and blocked checks; do not claim success from a build or landing page alone.
```

**Checkpoint:** A working database-backed form flow and retained data are
stronger evidence than an IIS welcome page. If execution is unavailable,
present the remaining manual steps as unverified.

## Scenario 2: WinForms with EF6

### Assess and stop

```text
Assess scenarios\02-winforms-ef6-products for containerization without edits
or setup. Inspect its startup and interactive UI dependencies, .NET Framework
4.8 requirements, EF6, and LocalDB configuration.
Explain whether the existing desktop UI is a supported container workload for
this demo. If unsupported, identify the evidence and stop: do not generate a
Dockerfile, replace the UI, extract a service, or upgrade the framework.
```

**Checkpoint:** The correct result is an explained blocker, not a runnable
Dockerfile. Windows containers do not make an interactive desktop UI a
supported workload for this demo.

### Explain the boundary

```text
Give me a short customer-facing explanation of why this WinForms sample stops
at assessment. Distinguish containerizing an existing application from extracting
a headless service or replacing its UI. Describe those as separate future
projects requiring explicit scope decisions, without implementing anything.
```

**Checkpoint:** There is no approval-to-implement stage for this sample.
Use the other scenarios to demonstrate supported containerization.

## Scenario 3: IIS-hosted WCF service

### Assess

```text
Assess scenarios\03-wcf-products-service\ProductsService for containerization.
Use scenarios\03-wcf-products-service as the candidate build context and verify
the actual solution/project dependencies before recommending it.
Preserve .NET Framework 4.8, EF6, SOAP contracts, bindings, and IIS hosting.
Outline a Windows Dockerfile with required IIS/WCF features, runtime startup,
configuration, ports, and host compatibility. Identify LocalDB and database
initialization constraints. Keep ProductsService.Client as a local verification
consumer, not another container. Provide a file-by-file outline and validation
plan; make no changes and stop for approval.
```

**Checkpoint:** Expect WCF activation requirements and SQL connectivity to be
addressed, not a REST, gRPC, or CoreWCF migration.

### Review dependencies and client configuration

```text
Review the WCF outline before implementation. Identify the non-secret details
needed for an accessible, dedicated demo SQL Server database and the agreed
runtime connection-configuration mechanism. Do not provision services or assume
integrated authentication works inside the container.
Explain how the local ProductsService.Client will target the published
ProductService.svc endpoint while preserving its SOAP binding and contract.
Inspect its App.config and interactive Console.ReadKey behavior when proposing
verification; do not silently rewrite the client for automation.
Revise the outline for necessary configuration changes, then stop for approval.
```

**Checkpoint:** Resolve database prerequisites and endpoint configuration.
The host-published service address is not necessarily the container's address
or the client's existing localhost port.

### Approve and implement

Use only after accepting the latest outline and resolving its prerequisites:

```text
I approve the latest reviewed WCF outline for scenarios\03-wcf-products-service,
including the agreed service runtime configuration and local client endpoint
configuration. Implement only those changes, create the service Dockerfile and
necessary supporting assets, and document PowerShell build/run/client commands.
Preserve the framework, EF6, SOAP contracts, and bindings; do not containerize
or refactor the client. Stop for revised approval if broader changes are needed.
```

**Checkpoint:** The container hosts the service. The existing console client
demonstrates contract compatibility from outside the container.

### Verify

```text
Verify the approved WCF container using compatible Windows Docker hosting and
the configured disposable demo database, if available. Build and start it,
inspect logs, and check ProductService.svc?wsdl.
Use the existing local console client to exercise GetProducts, CreateProduct,
and GetProduct against the published endpoint, accounting for its interactive
key press rather than hanging an unattended shell. Check that the created
product remains available after replacing only the demo service container.
Report exact commands, observed results, and blocked/manual steps. WSDL alone
is not proof that database-backed SOAP operations work.
```

**Checkpoint:** Show a real SOAP read/write round trip, not just service
metadata. Do not claim an interactive step ran if it was only documented.

## Scenario 4: ASP.NET Core 6 API

### Assess and review

```text
Assess scenarios\04-aspnetcore6-webapi-products without modifying files or
running setup. Inspect and reuse its existing Dockerfile; do not create a
competing Dockerfile. Preserve .NET 6, package versions, Startup.cs, and API
behavior, while clearly flagging the EOL runtime risk.
Outline minimal container changes, verified compatible image tags, build
context, runtime bind address/port, and HTTPS redirection implications.
Inspect ConnectionStrings:ProductsDb and propose SQLite persistence with
appropriate write permissions. Do not rely on Development-only Swagger for
readiness. Include the existing test project and real /api/products operations
in the validation plan. Present a file-by-file outline and stop for approval.
```

**Checkpoint:** Review persistence and port configuration carefully. An image
that starts but stores `products.db` only in its writable layer loses that
data when the container is replaced. Runtime modernization stays out of scope.

### Approve and implement

Use only after resolving the outline's storage, configuration, and host decisions:

```text
I approve the latest reviewed outline for scenarios\04-aspnetcore6-webapi-products.
Update the existing Dockerfile and only the agreed supporting/configuration
files. Preserve .NET 6, dependencies, Startup.cs, and API contracts.
Document exact PowerShell build/run commands, context, published ports, SQLite
mount and connection configuration, and required write permissions.
Do not upgrade the runtime or introduce schema migrations. Stop for revised
approval if new blockers require additional changes.
```

**Checkpoint:** Review the existing Dockerfile's diff and distinguish runtime
configuration from source changes. Demo acceptance does not remove EOL risk.

### Verify

```text
Verify the approved API container setup if compatible Docker hosting is available.
Run the existing ProductsApi.Tests\ProductsApi.Tests.csproj tests from
scenarios\04-aspnetcore6-webapi-products, then build/run the documented image.
Use a disposable SQLite volume and test GET /api/products, POST /api/products
with a valid uniquely named product, and GET /api/products/{id} using the ID
returned by POST. Check response statuses and values, not just connectivity.
Replace only the demo container while reusing that volume and confirm the
created product remains. Check logs, binding, and HTTPS behavior.
Distinguish controller test results from container/database integration results;
report exact commands, evidence, and anything blocked or unverified.
```

**Checkpoint:** Existing InMemory controller tests are useful but do not
validate SQLite persistence or Docker networking. The container checks do.

## Troubleshooting prompts

### Missing Docker, incompatible host, or missing services

```text
Separate the current demo's completed assessment and authored assets from its
blocked runtime checks. Explain the missing tool, host compatibility, or service
prerequisite and give the remaining PowerShell commands with working directories
and expected outcomes. Do not install tools, switch engine modes, provision
infrastructure, change frameworks, or claim unexecuted checks passed.
```

**Checkpoint:** A clear blocked result is a valid assessment demo, not a
completed runnable-container demo.

### Build or startup failure

```text
Diagnose the current demo failure using the actual build output, container logs,
and approved configuration without exposing secrets. Identify the root cause.
Fix it only if the change is within the approved outline; otherwise present
the additional file changes and reasons and stop for explicit approval.
Do not suppress failures, weaken checks, or substitute a successful-looking
fallback. Re-run the failing check after any approved fix.
```

**Checkpoint:** A new portability blocker should produce a revised outline,
not an unannounced framework or architecture change.

## Wrap up and cleanup

```text
Summarize this sample's containerization demo for the audience.
List each changed file and why it was necessary, identify which application
source changes were avoided, and show the final build context and run commands.
Separate authored Dockerfile, successful image build, and verified application
behavior. List unresolved risks and checks not run. Do not describe this as a
.NET 10 upgrade or production-ready deployment.
```

**Checkpoint:** Compare the resulting diff with the approved outline. For
WinForms, explain the unsupported workload and absence of container changes.

Before cleanup, identify and review the exact resources created for this demo:

```text
List the exact IDs/names of containers, images, volumes, networks, and test data
created for this demo, with how each was identified. Propose scoped cleanup
commands without executing them. Keep repository changes for review and preserve
all unrelated resources; do not use broad prune, wildcard removal, or git reset.
```

**Checkpoint:** After reviewing the inventory, explicitly authorize only the
specific resources you want removed. Removing a demo database or volume destroys
its test data; retain it if it is needed for another demonstration.
