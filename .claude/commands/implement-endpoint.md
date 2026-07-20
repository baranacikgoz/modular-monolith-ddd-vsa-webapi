---
description: Scaffold REPR endpoint files (Endpoint.cs, Request.cs with inline RequestValidator, Response.cs) and register in Setup.cs.
argument-hint: "<Module> <Aggregate> <Feature> [READ|WRITE]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Target: $ARGUMENTS

1. **Create files** in `src/Modules/{Module}/{Module}.Endpoints/{Aggregate}/v1/{Feature}/`:
   - `Endpoint.cs` — static class with `MapEndpoint(RouteGroupBuilder)` and a static handler returning `Task<Result>` or `Task<Result<Response>>`
   - `Request.cs` — sealed record with `[FromRoute]`/`[FromBody]` parameters and `required` properties, **plus** a `sealed class RequestValidator : CustomValidator<Request>` appended at the bottom of the same file (injecting `IResxLocalizer`) — validators are not a separate file. Validation auto-fires via `AddFluentValidationAutoValidation()`, registered once at module root; no per-endpoint wiring needed.
   - `Response.cs` — sealed record with `required` properties (omit for no-content writes; keep for writes that return a created/updated payload, e.g. via `TransformResultToCreatedResponse<Response>`)

2. **Handler pattern**:
   - Write: `.SingleAsResultAsync(nameof({Aggregate}), cancellationToken)` → `.TapAsync(agg => agg.Method(...))` → `.TapAsync(_ => db.SaveChangesAsync(cancellationToken))`
   - Read: `.AsNoTracking()` → `.Select(x => new Response { ... })` → `.SingleAsResultAsync(nameof({Aggregate}), cancellationToken)`
   - No `Find`/`FirstOrDefault`. No mapping libraries. No imperative `if (result.IsFailure)` checks.

3. **Domain method (writes only)**: ensure the Aggregate has the method; it must call `RaiseEvent(new DomainEvent(...))`.

4. **Telemetry**: add `ActivitySource` instrumentation only if it provides meaningful observability value.

5. **Register** in the feature's `Setup.cs`. Do not call `.MapToApiVersion(...)` here — versioning is applied once at the module root (`{Module}Module.cs`: `endpoints.MapGroup("/v{version:apiVersion}").WithApiVersionSet(apiVersionSet)`):
   ```csharp
   var group = app.MapGroup("resource").WithTags("Resources");
   Feature.Endpoint.MapEndpoint(group);
   ```

6. **Build**: run `make build` to confirm zero warnings and zero errors.
