---
description: Reproduce, diagnose, and fix a bug using the scientific Red/Green method — no guesswork.
argument-hint: "<Module> [description or trace ID]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

ultrathink

Fix bug in: $ARGUMENTS

**Phase 1 — Reproduce (Red test)**

Do NOT touch production code yet.

1. Search for keywords from the error/trace to locate the specific Endpoint, Aggregate, or Service.
2. If a Trace ID is provided, look up the failing OTel span in the Aspire dashboard (local) or your environment's trace backend (dev/prod). Traces are not stored as files under `src/Common`/`src/Modules`.
3. Create: `src/Modules/{Module}/{Module}.Tests/Bugs/Issue_{Timestamp}.cs`
4. Use Bogus to recreate the exact state from the bug report.
5. Write a test asserting the **expected** (correct) behavior.
6. Run `make test-{module}`. The test **must fail**. If it passes, the bug is not reproduced — ask for more context.

**Phase 2 — Diagnose**

Step through the failing logic and check:
- Invariant violations in the Domain Aggregate
- EF Core projection errors (missing `Include`, wrong `Select` mapping)
- Messaging race conditions in the Outbox
- Validation gaps allowing invalid state through

**Phase 3 — Fix (Green move)**

1. Apply the **minimal** code change fixing the root cause.
2. Confirm the fix violates no architectural rules.
3. Run the reproduction test — it **must pass**.

**Phase 4 — Regression & cleanup**

1. `make test-{module}` — all existing tests must still pass.
2. Move the reproduction test to `src/Modules/{Module}/{Module}.Tests/Endpoints/`.
3. Delete the temporary `Bugs/Issue_{Timestamp}.cs`.
4. Report: root cause, fix applied, test that now prevents regression.

**CI-only failures (passes locally, fails only in CI) — do not reason from the stack trace**

Local-green/CI-red means the local box is masking the cause (a running broker, more CPU, config that only diverges under CI). One instrumented CI run beats three reasoned guesses:

1. Instrument the failing path to emit the CI environment's real state (e.g. for a DB lock/timeout, dump `pg_stat_activity` and `pg_blocking_pids(...)` plus effective option values from inside the `catch`).
2. Throw that state into the exception message — xUnit swallows `Console` output, so it must be in the assertion/exception text to show up in the CI log.
3. Push, let CI run, read the dumped state from the CI log, then fix from that evidence — not from the original stack trace alone.
