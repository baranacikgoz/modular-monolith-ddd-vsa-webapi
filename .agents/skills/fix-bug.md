---
description: Reproduce, diagnose, and fix a bug using the scientific Red/Green method — no guesswork.
---

Reproduce, diagnose, and fix a bug using the scientific method. Ask for the bug description, suspected module, and any logs/trace IDs if not provided.

**Phase 1 — Reproduce (Red test)**

Do NOT touch production code yet.

1. Search the codebase for keywords from the error message or stack trace to locate the specific Endpoint, Aggregate, or Service involved.
2. If a Trace ID is provided, use it to find the failing OTel span in `src/Common` or `src/Modules` logs.
3. Create a new test file: `src/Modules/{Module}/{Module}.Tests/Bugs/Issue_{Timestamp}.cs`
4. Use Bogus to recreate the exact state described in the bug report.
5. Write a test that asserts the **expected** (correct) behavior.
6. Run `make test-{module}`. The test **must fail**. If it passes, the bug is not reproduced — ask the user for more context.

**Phase 2 — Diagnose**

With the Red test in place, step through the failing logic and check:
- Invariant violations in the Domain Aggregate
- EF Core projection errors (missing `Include`, wrong `Select` mapping)
- Messaging race conditions in the Outbox
- Validation gaps allowing invalid state through

**Phase 3 — Fix (Green move)**

1. Apply the **minimal** code change that fixes the root cause.
2. Confirm the fix does not violate architectural rules (no new cross-module refs, no mapping library, no direct bus publish from write path).
3. Run the reproduction test. It **must pass**.

**Phase 4 — Regression & cleanup**

1. Run `make test-{module}` for the full module suite — all existing tests must still pass.
2. Move the reproduction test into the permanent test suite (e.g. `src/Modules/{Module}/{Module}.Tests/Endpoints/`).
3. Delete the temporary `Bugs/Issue_{Timestamp}.cs` file.
4. Report: root cause, the fix applied, and the test that now prevents regression.
