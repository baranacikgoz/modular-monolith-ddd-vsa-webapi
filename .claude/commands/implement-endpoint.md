---
description: Scaffold REPR endpoint files (Endpoint.cs, Request.cs, Response.cs, RequestValidator.cs) and register in Setup.cs.
argument-hint: "<Module> <Aggregate> <Feature> [READ|WRITE]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Target: $ARGUMENTS

1. **Create files** in `src/Modules/{Module}/{Module}.Endpoints/{Aggregate}/v1/{Feature}/`:
   - `Endpoint.cs` — static class with `MapEndpoint(RouteGroupBuilder)` and a static handler returning `Task<Result>` or `Task<Result<Response>>`
   - `Request.cs` — sealed record with `[FromRoute]`/`[FromBody]` parameters and `required` properties
   - `Response.cs` — sealed record with `required` properties (omit for WRITE)
   - `RequestValidator.cs` — `sealed class` extending `CustomValidator<Request>`, injecting `IResxLocalizer`

2. **Handler pattern**:
   - Write: `.SingleAsResultAsync(nameof({Aggregate}), cancellationToken)` → `.TapAsync(agg => agg.Method(...))` → `.TapAsync(_ => db.SaveChangesAsync(cancellationToken))`
   - Read: `.AsNoTracking()` → `.Select(x => new Response { ... })` → `.SingleAsResultAsync(nameof({Aggregate}), cancellationToken)`
   - No `Find`/`FirstOrDefault`. No mapping libraries. No imperative `if (result.IsFailure)` checks.

3. **Domain method (writes only)**: ensure the Aggregate has the method; it must call `RaiseEvent(new DomainEvent(...))`.

4. **Telemetry**: add `ActivitySource` instrumentation only if it provides meaningful observability value.

5. **Register** in the feature's `Setup.cs`:
   ```csharp
   var v1Group = routeBuilder.MapGroup("/resource").WithTags("Tag").MapToApiVersion(1);
   v1.Feature.Endpoint.MapEndpoint(v1Group);
   ```

6. **Build**: run `make build` to confirm zero warnings and zero errors.
