Plan a new feature end-to-end. Produce a detailed plan suitable for a PR description.

1. **Understand the request**: clarify scope with the user if anything is ambiguous.

2. **Identify boundaries**: determine which module(s) this feature belongs to. If it spans modules, plan the `IntegrationEvents` needed for async communication and the `InterModuleRequests` for any sync calls.

3. **File inventory**: list every file to be created or modified, with a one-line description of each change.

4. **Data structures**: define new Aggregates, Entities, Value Objects, Domain Events, DTOs, and DB schema changes (migrations).

5. **Testing strategy**: specify which unit tests and integration tests are required. Note what the write-side assertions verify (entity in DB + OutboxMessages record) and what the read-side assertions verify (response DTO shape).

6. **Defensive strategy**: identify inbound validation rules (FluentValidation) and any 3rd-party integrations requiring resiliency (retry, circuit breaker, response validation).

7. **Telemetry plan**: list any `ActivitySource` spans and `Meter` instruments that provide meaningful observability — skip if the overhead outweighs the insight.

8. **Output**: write the complete plan as a structured markdown document. This becomes the PR description when implementation begins.
