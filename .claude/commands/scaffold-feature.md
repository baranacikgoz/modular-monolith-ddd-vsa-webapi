---
description: Scaffold a vertical slice (Endpoint, Request with inline validator, Response, domain method) and register it in Setup.cs.
argument-hint: "<Module> <Aggregate> <Feature> READ|WRITE"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold: $ARGUMENTS

Copy the shape from the CLAUDE.md §2 exemplars (Activate for WRITE, Get for READ, Search for paginated). Rules in CLAUDE.md §3, §4, §5.

1. Create `src/Modules/{Module}/{Module}.Endpoints/{Aggregate}/v1/{Feature}/`:
   - `Endpoint.cs`: `internal static class Endpoint` with `MapEndpoint(RouteGroupBuilder)`. Chain: `.WithDescription(...)`, `.RequireScope(KeycloakScopes.{Aggregate}s.{Action})`, `.Produces(...)`, then `.TransformResultToNoContentResponse()` (write), `.TransformResultTo<Response>()` (read) or `.TransformResultToCreatedResponse<Response>()` (create).
   - `Request.cs`: always create it when the endpoint takes any id, query, or body. Sealed record, `required` properties, every property carries `[FromRoute]`, `[FromQuery]`, or `[FromBody]` explicitly. Strongly-typed ids: `[ModelBinder<StronglyTypedIdBinder<TId>>]`. Append `public sealed class RequestValidator : CustomValidator<Request>` (or `PaginationRequestValidator<Request>`) in the same file, injecting `IResxLocalizer`. An empty validator body is fine; the class must exist. Skip `Request.cs` only for endpoints with no inputs at all (`ICurrentUser`-only reads).
   - `Response.cs`: sealed record with `required` properties. Omit for no-content writes.
2. Handler binding: `[AsParameters] Request request` whenever `Request` has any `[FromRoute]` or `[FromQuery]` property. A pure `[FromBody]` request binds as a plain parameter. Without `[AsParameters]`, Minimal APIs treat the record as a JSON body and route/query values never bind.
3. WRITE: add `{Aggregate}.{Feature}(...)` to the aggregate. Build the `V1...DomainEvent` first, mutate, then `RaiseEvent` (§6).
4. Register in `{Aggregate}/Setup.cs`: `v1.{Feature}.Endpoint.MapEndpoint({aggregate}ApiGroup);`. Create the `Map{Aggregate}sEndpoints` extension and call it from `{Module}Module.MapEndpoints` if the aggregate is new.
5. Add the scope to `keycloak/realm-modular-monolith.json` and `KeycloakScopes` if it does not exist.
6. `make build`, zero warnings. Then `/scaffold-test {Module} {Feature} READ|WRITE`.
