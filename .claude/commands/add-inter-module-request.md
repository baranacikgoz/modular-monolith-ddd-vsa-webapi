---
description: Declare an InterModuleRequest contract in Common, implement the handler in the source module, and wire the caller injection.
argument-hint: "<SourceModule> <RequestName>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Add inter-module request: $ARGUMENTS

**Step 1 — Contract** in `src/Common/Common.InterModuleRequests/{SourceModule}/{RequestName}.cs`:
```csharp
public sealed record {RequestName}Request(/* params */) : IInterModuleRequest<{RequestName}Response>;
public sealed record {RequestName}Response(/* params */);
```

**Step 2 — Handler** in `src/Modules/{SourceModule}/{SourceModule}.Infrastructure/InterModuleRequestHandlers/{RequestName}RequestHandler.cs`:
```csharp
public class {RequestName}RequestHandler(I{SourceModule}DbContext db)
    : InterModuleRequestHandler<{RequestName}Request, {RequestName}Response>
{
    public override async Task<{RequestName}Response> HandleAsync(
        {RequestName}Request request,
        CancellationToken cancellationToken)
    {
        // query db, build and return response
        return new {RequestName}Response(...);
    }
}
```

**Step 3 — No manual registration.** Consumers auto-register via assembly scan (`x.AddConsumers(moduleAssemblies)` in `src/Host/Host/Infrastructure/Setup.MassTransit.cs`). There is no `ModuleInstaller.cs` file in this repo — skip this step entirely once the handler class exists.

**Step 4 — Caller**: in the consuming module, inject `IInterModuleRequestClient<{RequestName}Request, {RequestName}Response>` and call:
```csharp
var response = await _client.SendAsync(new {RequestName}Request(...), cancellationToken);
```

**Step 5** — `make build` — zero warnings.
