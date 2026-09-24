# Scenario 3: WCF SOAP service → .NET 10 migration candidate

`ProductsService` (an IIS-hosted WCF service) and `ProductsService.Client`
(a console consumer) are a small, intentionally "legacy" pair of apps
targeting **.NET Framework 4.8**. Together they demonstrate a
service-oriented migration candidate for **.NET 10** -- a distinct and
higher-difficulty story than scenarios 1 and 2, because **WCF has no
direct server-side successor in modern .NET**. A real migration has to
choose between:

- [CoreWCF](https://github.com/CoreWCF/CoreWCF) -- a community-maintained,
  drop-in-ish WCF server implementation for ASP.NET Core, best when SOAP
  interop must be preserved exactly.
- gRPC -- a modern RPC framework with strong typing and codegen, if
  clients can be updated to a new wire format.
- A REST/minimal API rewrite -- if the operations map cleanly to
  resource-oriented HTTP and SOAP/WSDL semantics aren't required anymore.

## What the apps do

Same Products domain as [scenario 1](../01-webforms-ef6-products/README.md)
and [scenario 2](../02-winforms-ef6-products/README.md), exposed over SOAP:

- **`ProductsService`** -- `IProductService` contract with
  `GetProducts`, `GetProduct`, `CreateProduct`, `UpdateProduct`, and
  `DeleteProduct` operations, hosted via `ProductService.svc` in IIS.
  Backed by its own EF6 Code-First `Product`/`ProductsContext` (own copy,
  consistent with scenario 2's approach) against SQL Server LocalDB.
- **`ProductsService.Client`** -- a console app with a hand-written,
  svcutil-style generated proxy (`ServiceReference/`) that calls the
  service end-to-end: lists products, creates one, and fetches it back.

## Intentional legacy patterns (migration targets)

| Pattern | Where | Why it matters for migration |
|---|---|---|
| `<system.serviceModel>` config-driven service definition | `ProductsService/Web.config` | Services/bindings/behaviors defined in XML have no equivalent shape in ASP.NET Core; CoreWCF partially preserves it, gRPC/minimal APIs replace it with code-first routing entirely. |
| `.svc` file + `ServiceHost` factory model | `ProductService.svc` | IIS-specific hosting mechanism; ASP.NET Core hosts everything through the generic host/middleware pipeline instead. |
| `[ServiceContract]`/`[OperationContract]`/`[DataContract]` attributes | `IProductService.cs`, `ProductDto.cs` | Attribute-driven, contract-first SOAP style; meaningless for gRPC (protobuf contracts) or REST (no WSDL/contract-first semantics). |
| Client-side generated proxy (`ClientBase<T>`) + matching client config | `ProductsService.Client/ServiceReference/`, `App.config` | Replaced by typed `HttpClient`/gRPC client patterns; the client and server configs must be kept in sync by hand today, a fragile pattern worth calling out. |
| SOAP/XML wire format via `basicHttpBinding` | `Web.config`, `App.config` | Migrating to REST/JSON or gRPC/protobuf changes the wire contract, not just the transport/hosting. |
| Per-call `new ProductsContext()` (no request-scoped DI) | `ProductService.svc.cs` | WCF's default per-call instancing has no natural DI scope; ASP.NET Core/EF Core would use `AddDbContext` + constructor injection instead. |
| EF6 Code-First (same as scenarios 1 & 2) | `Models/`, `Migrations/` | Consistent EF6 → EF Core story across all three app types. |

## Running locally (Windows + Visual Studio)

1. Requires Visual Studio 2019+ with the **ASP.NET and web development**
   workload, and **SQL Server Express LocalDB**.
2. Open `ProductsServiceSolution.sln`, restore NuGet packages
   (`EntityFramework` 6.4.4).
3. Run `ProductsService` first (F5 with it as startup project) so IIS
   Express hosts it and EF6 seeds the LocalDB database. Confirm it's up
   by browsing to `http://localhost:44301/ProductService.svc` (shows the
   WCF "service is running" help page) or `?wsdl` for the WSDL.
4. Then run `ProductsService.Client` (set as startup project) to exercise
   the service end-to-end from the console. Update the endpoint address
   in `App.config` if IIS Express assigns a different port.

## Not included (by design, scenario is kept small)

Authentication/message security, `wsHttpBinding`/`netTcpBinding`
alternatives, and fault contracts are intentionally left out to keep this
scenario focused on the core WCF hosting/contract/proxy patterns above.
