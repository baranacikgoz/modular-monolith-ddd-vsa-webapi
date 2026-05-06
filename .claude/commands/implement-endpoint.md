Implement a new REPR-pattern endpoint following this sequence:

1. **Create files** in `src/Modules/{module}/Endpoints/{Aggregate}/v1/{Feature}/`:
   - `Endpoint.cs` — static class with `MapEndpoint(RouteGroupBuilder)` and a static handler returning `Task<Result>` or `Task<Result<Response>>`
   - `Request.cs` — sealed record with `[FromRoute]`/`[FromBody]` parameters and `required` properties
   - `Response.cs` — sealed record with `required` properties
   - `RequestValidator.cs` — `sealed class` extending `CustomValidator<Request>`, injecting `IResxLocalizer`

2. **Handler pattern**:
   - Write: `.SingleAsResultAsync(nameof(Entity), cancellationToken)` → `.TapAsync(agg => agg.Method(...))` → `.TapAsync(_ => db.SaveChangesAsync(cancellationToken))`
   - Read: `.AsNoTracking()` → `.Select(x => new Response { ... })` → `.SingleAsResultAsync(nameof(Entity), cancellationToken)`
   - No `Find`/`FirstOrDefault`. No mapping libraries. No imperative `if (result.IsFailure)` checks.

3. **Domain method (writes only)**: ensure the Aggregate has the method; it must call `RaiseEvent(new DomainEvent(...))`.

4. **Telemetry**: add `ActivitySource` instrumentation only if it provides meaningful observability value.

5. **Register** in the feature's `Setup.cs`:
   ```csharp
   var v1Group = routeBuilder.MapGroup("/resource").WithTags("Tag").MapToApiVersion(1);
   v1.Feature.Endpoint.MapEndpoint(v1Group);
   ```

6. **Build**: run `make build` to confirm zero warnings and zero errors before finishing.
