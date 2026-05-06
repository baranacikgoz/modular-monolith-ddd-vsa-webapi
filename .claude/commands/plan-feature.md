---
description: Plan a new feature end-to-end — module boundaries, integration events, file inventory, testing and telemetry strategy.
argument-hint: "<feature description>"
context: fork
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Plan this feature: $ARGUMENTS

1. **Identify boundaries**: determine which module(s) own this feature. If it spans modules, identify the `IntegrationEvents` required for async communication and any `InterModuleRequests` for sync calls.

2. **File inventory**: list every file to be created or modified, with a one-line description of the change.

3. **Data structures**: define new Aggregates, Entities, Value Objects, DomainEvents, DTOs, and DB schema changes (migrations needed?).

4. **Testing strategy**: specify unit tests and integration tests required.
   - Write assertions: entity in DB + record in `OutboxMessages`.
   - Read assertions: response DTO shape matches seeded data.

5. **Defensive strategy**: identify FluentValidation rules for inbound `Request`s. Identify any 3rd-party integrations requiring resiliency (retry, circuit breaker, response validation).

6. **Telemetry plan**: list `ActivitySource` spans and `Meter` instruments that provide meaningful observability — skip if the overhead outweighs the insight.

7. **Output**: produce a structured markdown plan suitable for a PR description. Implementation begins only after this plan is approved.
