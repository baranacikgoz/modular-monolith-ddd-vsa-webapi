---
description: Implement a planned feature end-to-end with full architectural compliance.
---

Implement a planned feature. The plan must already exist (PR description, issue, or previous conversation).

1. **Review plan**: read the approved plan to understand exact scope and architectural decisions.

2. **Green baseline**: run `make test-{module}` on the target module to confirm existing tests pass before touching anything.

3. **Domain layer**: implement or update Aggregates/Entities. Add domain methods that call `RaiseEvent(new DomainEvent(...))`. Update `ApplyEvent` for state mutations.

4. **Application/Infrastructure layer**: implement any internal services, repository logic, or infrastructure changes defined in the plan. Follow the functional Result pipeline throughout.

5. **Cross-module integration** (if required): define `IntegrationEvents` in `Common` and implement MassTransit consumers. Never publish to the bus directly from Write-side command paths — use `RaiseEvent`.

6. **Endpoints**: run the `implement-endpoint` skill for each new endpoint in the plan.

7. **Verification**: run the `verify-feature` skill to execute the full quality gate.

8. **Audit**: inspect all modified `.csproj` files to confirm no illegal cross-module references were introduced. Confirm `.AsNoTracking()` on all reads and no mapping library usage anywhere.
