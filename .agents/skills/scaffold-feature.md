---
description: Scaffold a new vertical slice (Endpoint + Domain method) for READ or WRITE operations.
---

Scaffold a new vertical slice. Ask for: module name, aggregate name, feature name, and type (READ or WRITE) if not provided.

**WRITE slice** — create `src/Modules/{Module}/Endpoints/{Aggregate}/v1/{Feature}/`:

`Endpoint.cs`:
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

**READ slice** — handler returns `Task<Result<Response>>`, uses `.AsNoTracking()`, projects inline to `Response`.

`Request.cs`:
```csharp
public sealed record Request([FromRoute] Guid Id, [FromBody] Body Body);
public sealed record Body(string Prop /* add fields */);
```

`Response.cs`:
```csharp
public sealed record Response { public required string Prop { get; init; } }
```

`RequestValidator.cs`:
```csharp
public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Body.Prop).NotEmpty().WithMessage(localizer.Module_Feature_Prop_Required);
    }
}
```

**Domain method (WRITE)**: ensure `{Aggregate}` has `{Feature}(...)` calling `RaiseEvent(new {Feature}Event(...))` and `ApplyEvent` handles the state mutation.

Run `make build` to confirm zero warnings.
