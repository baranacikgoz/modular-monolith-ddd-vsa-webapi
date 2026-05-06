---
description: Implement a planned feature end-to-end with full architectural compliance.
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Implement planned feature for module: $ARGUMENTS

1. **Review plan**: read the approved plan from the current conversation or PR description.

2. **Green baseline**: run `make test-{module}` to confirm existing tests pass before touching anything.

3. **Domain layer**: implement or update Aggregates/Entities. Domain methods must call `RaiseEvent(new DomainEvent(...))`. Update `ApplyEvent` for state mutations.

4. **Application/Infrastructure layer**: implement internal services and repository logic following the functional Result pipeline throughout.

5. **Cross-module integration** (if required): define `IntegrationEvents` in `Common.IntegrationEvents` and implement MassTransit consumers. Never publish to the bus directly from Write-side paths — use `RaiseEvent`.

6. **Endpoints**: run `implement-endpoint` skill for each new endpoint in the plan.

7. **Verification**: run `verify-feature {Module}` for the full quality gate.

8. **Audit**: inspect all modified `.csproj` files — zero cross-module references. Confirm `.AsNoTracking()` on all reads and no mapping library usage.
