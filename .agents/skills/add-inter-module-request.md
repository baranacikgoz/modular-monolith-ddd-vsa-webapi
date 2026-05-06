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

**Step 2 — Handler** in `src/Modules/{SourceModule}/{SourceModule}.Application/`:
```csharp
public class {RequestName}Handler(I{SourceModule}DbContext db)
    : InterModuleRequestHandler<{RequestName}Request, {RequestName}Response>
{
    protected override async Task<{RequestName}Response> HandleAsync(
        ConsumeContext<{RequestName}Request> context,
        {RequestName}Request request,
        CancellationToken cancellationToken)
    {
        return new {RequestName}Response(...);
    }
}
```

**Step 3 — Register**: in the source module's `ModuleInstaller.cs`, add `AddConsumer<{RequestName}Handler>()` to MassTransit configuration.

**Step 4 — Caller**: inject `IInterModuleRequestClient<{RequestName}Request, {RequestName}Response>` and call:
```csharp
var response = await _client.SendAsync(new {RequestName}Request(...), cancellationToken);
```

**Step 5** — `make build` — zero warnings.
