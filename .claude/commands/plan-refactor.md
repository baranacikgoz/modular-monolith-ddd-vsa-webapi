---
description: Plan a refactor: current violations, exact file changes, risks. No code until approved.
argument-hint: "<target scope and goal>"
context: fork
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Plan refactor: $ARGUMENTS

Output a markdown plan. Implementation starts only after approval.

1. Current state: run the `/audit-architecture` checks against the target scope and list each violation with `file:line`.
2. File inventory: every file to change, a short before excerpt, and the intended after shape (point to CLAUDE.md §2 exemplars).
3. Risks: outbox flow, shipped `V{n}` events (add `V{n+1}`, never edit), cross-module contracts, migrations, Keycloak scopes.
4. Order of batches so `make build` stays green between them.
