---
description: Reproduce, diagnose, and fix a bug with a Red/Green test. No guesswork.
argument-hint: "<Module> [description or trace ID]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

ultrathink

Fix bug in: $ARGUMENTS

Rules in CLAUDE.md §8 (bugs, CI-only failures).

1. Locate: graphify query on error keywords. With a trace ID, find the failing span in the Aspire dashboard or the environment's trace backend.
2. Red: `src/Modules/{Module}/{Module}.Tests/Bugs/Issue_{Timestamp}.cs`, Bogus state from the report, assert the correct behavior. `make test-{module}` must fail. If it passes, the bug is not reproduced; ask for more context. No production code changes yet.
3. Diagnose: aggregate invariants, EF projection (`Include`, `Select`), outbox races, validation gaps. Grep every caller of the function before editing; fix at the shared root, not the reported path.
4. Green: minimal root-cause fix that respects CLAUDE.md. Reproduction test passes.
5. `make test-{module}` green. Move the test into `{Module}.Tests/Endpoints/` (or the matching folder), delete `Bugs/`.
6. Report root cause, fix, and the regression test.
