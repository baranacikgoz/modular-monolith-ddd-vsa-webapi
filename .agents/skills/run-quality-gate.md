---
description: Run tests + architecture audit. Fix failures before reporting completion.
---

Run the full quality gate. Fix any failures before reporting completion.

1. **Architecture audit**: run the `audit-architecture` skill and resolve all FAILs before proceeding.

2. **Architecture tests**: if a `Tests/Architecture.Tests` project exists, run it first. These contain NetArchTest rules (e.g. "Domain cannot depend on Infrastructure").

3. **Full test suite**:
   ```bash
   make test
   ```
   Tests run sequentially to prevent Docker resource exhaustion. Run module-specific targets for faster iteration (e.g. `make test-iam`).

4. **Failure handling**: if any test fails, analyze the stack trace and fix the implementation. Do NOT change the test to make it pass — fix the code.

5. **Report**: summarize pass/fail counts per module and confirm the overall exit code is 0.
