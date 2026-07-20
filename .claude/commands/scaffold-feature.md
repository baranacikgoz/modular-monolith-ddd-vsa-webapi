---
description: Scaffold a full vertical slice — Endpoint, Request, Response, Validator, and Domain method.
argument-hint: "<Module> <Aggregate> <Feature> READ|WRITE"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold: $ARGUMENTS

Create `src/Modules/{Module}/{Module}.Endpoints/{Aggregate}/v1/{Feature}/`.

**WRITE slice — `Endpoint.cs`:**
```csharp
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPut("{Aggregate}s/{id}", HandleAsync)
             .WithDescription("{Feature} action")
             .Produces(StatusCodes.Status204NoContent)
             .TransformResultToNoContentResponse();
    }

    private static async Task<Result> HandleAsync(
        [AsParameters] Request request,
        [FromServices] I{Module}DbContext db,
        CancellationToken cancellationToken)
    {
        return await db.{Aggregate}s
            .TagWith(nameof(HandleAsync), request.Id)
            .Where(x => x.Id == request.Id)
            .SingleAsResultAsync(nameof({Aggregate}), cancellationToken)
            .TapAsync(agg => agg.{Feature}(request.Body.Data))
            .TapAsync(_ => db.SaveChangesAsync(cancellationToken));
    }
}
```

**READ slice** — handler returns `Task<Result<Response>>`, uses `.AsNoTracking()`, projects inline to `Response` via `.Select(...)`.

**`Request.cs`** — **always scaffold this file, no exception**, even for an Id-only request or an empty pagination-only request (empty `RequestValidator` body is fine, but the class stays). Record plus an inline validator appended at the bottom of the same file (not a separate `RequestValidator.cs`). Validation auto-fires via `AddFluentValidationAutoValidation()`, registered once at module root — no per-endpoint wiring:
```csharp
public sealed record Request([FromRoute] Guid Id, [FromBody] Body Body);
public sealed record Body(string Prop /* add fields */);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Body.Prop).NotEmpty().WithMessage(localizer.Module_Feature_Prop_Required);
    }
}
```

Strongly-typed route/query ids use `[ModelBinder<StronglyTypedIdBinder<TId>>]` instead of raw `Guid`.

**`[AsParameters]` rule**: only bind `Request` via `[AsParameters]` when it mixes binding sources (route+body, route+query, as in the WRITE example above). A pure-body Request (Create with only `[FromBody]` fields) or an endpoint with no Request at all (`ICurrentUser`-only read) binds the parameter directly — no `[AsParameters]`.

**`Response.cs`:**
```csharp
public sealed record Response { public required string Prop { get; init; } }
```

**Domain method (WRITE)**: ensure `{Aggregate}` has `{Feature}(...)` calling `RaiseEvent(new {Feature}Event(...))` and `ApplyEvent` handles the state mutation.

**Register** in the feature's `Setup.cs` — do not call `.MapToApiVersion(...)` here, versioning is applied once at the module root:
```csharp
v1.{Feature}.Endpoint.MapEndpoint(group);
```

Run `make build` to confirm zero warnings.
