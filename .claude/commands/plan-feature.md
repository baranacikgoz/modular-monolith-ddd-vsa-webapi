---
description: Plan a feature: owning module, cross-module contracts, file inventory, data model, tests, telemetry. No code until approved.
argument-hint: "<feature description>"
context: fork
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Plan this feature: $ARGUMENTS

Output a markdown plan usable as a PR description. Implementation starts only after approval.

1. Owning module(s). If it spans modules: which `IntegrationEvent`s (async) and `InterModuleRequest`s (sync).
2. File inventory: every file created or modified, one line each, using the CLAUDE.md §1 layout.
3. Data model: aggregates, entities, value objects, `V1` domain events (snapshot types per §6), DTOs, migrations.
4. Tests: integration (write = DB row + `OutboxMessages`; read = response matches seed) and aggregate unit tests.
5. Defense: validation rules per `Request`; third-party calls needing retry, circuit breaker, response validation; new Keycloak scopes; new Options (§9).
6. Telemetry: spans and meters worth their overhead, or none.
