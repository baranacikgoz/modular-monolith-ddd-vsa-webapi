---
description: Implement an approved feature plan end-to-end.
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Implement planned feature for: $ARGUMENTS

1. Read the approved plan from the conversation or PR description.
2. Green baseline: `make test-{module}`.
3. Optional Red phase: `/scaffold-test {Module} {Feature} READ|WRITE red`.
4. Domain: aggregates, events, methods per CLAUDE.md §6.
5. Cross-module: `/add-integration-event` (async) or `/add-inter-module-request` (sync) as the plan requires.
6. Endpoints: `/scaffold-feature` per endpoint. Migrations: `/manage-migration`.
7. Tests: `/scaffold-test` for every new behavior.
8. `/verify-feature {Module}`.
