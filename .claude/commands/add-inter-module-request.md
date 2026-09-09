---
description: Declare an InterModuleRequest contract, implement the handler in the source module, and wire the caller.
argument-hint: "<SourceModule> <RequestName>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Add inter-module request: $ARGUMENTS

Rules in CLAUDE.md §6. Exemplar: `GetStockLevel` (§2).

1. `src/Common/Common.InterModuleRequests/{Source}/{Name}.cs`: `{Name}Request(...) : IInterModuleRequest<{Name}Response>` and `{Name}Response(...)`.
2. `src/Modules/{Source}/{Source}.Infrastructure/InterModuleRequestHandlers/{Name}RequestHandler.cs`: `InterModuleRequestHandler<{Name}Request, {Name}Response>`, override `HandleAsync`, read with `.AsNoTracking()`.
3. No registration step; the handler is a MassTransit consumer and assembly scan picks it up.
4. Caller: inject `IInterModuleRequestClient<{Name}Request, {Name}Response>`, call `SendAsync(request, cancellationToken)`.
5. `make build`. Tests for the caller module must list the source module in `GetActiveModules()`.
