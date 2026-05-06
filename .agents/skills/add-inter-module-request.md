---
description: Add an InterModuleRequest contract and handler for sync cross-module communication.
---

Add a new InterModuleRequest for sync cross-module communication. Ask for source module (the one that owns and handles the request), request name, and payload if not provided.

**Step 1 — Declare the contract** in `src/Common/Common.InterModuleRequests/{SourceModule}/{RequestName}.cs`:
```csharp
public sealed record {RequestName}Request(/* params */) : IInterModuleRequest<{RequestName}Response>;
public sealed record {RequestName}Response(/* params */);
```

**Step 2 — Implement the handler** in `src/Modules/{SourceModule}/{SourceModule}.Application/{HandlerFile}.cs`:
```csharp
public class {RequestName}Handler(I{SourceModule}DbContext db)
    : InterModuleRequestHandler<{RequestName}Request, {RequestName}Response>
{
    protected override async Task<{RequestName}Response> HandleAsync(
        ConsumeContext<{RequestName}Request> context,
        {RequestName}Request request,
        CancellationToken cancellationToken)
    {
        // query db, build response
        return new {RequestName}Response(...);
    }
}
```

**Step 3 — Register the handler** in the source module's `ModuleInstaller.cs` via MassTransit `AddConsumer<{RequestName}Handler>()`.

**Step 4 — Use from the caller module**: inject `IInterModuleRequestClient<{RequestName}Request, {RequestName}Response>` and call:
```csharp
var response = await _client.SendAsync(new {RequestName}Request(...), cancellationToken);
```

**Step 5** — run `make build` to confirm zero warnings.
