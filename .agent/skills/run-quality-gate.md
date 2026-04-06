skill:
  name: "run-quality-gate"
  description: "Runs Unit Tests, Integration Tests, and Architecture Rules."
  inputs: []

  instructions: |
    1. **Architecture Audit**:
       - Execute `audit-architecture` skill (internal check).

    2. **Run Architecture Tests**:
       - If a `Tests/Architecture.Tests` project exists, run it.
       - *Note*: This project typically contains `NetArchTest` rules like "Domain cannot depend on Infrastructure".

    3. **Run Suite**:
       - Command: `make test` (runs all module tests sequentially to prevent Docker resource exhaustion).
       - You can also run module-specific tests directly (e.g. `make test-iam`, `make test-products`, `make test-common`).

    4. **Report**:
       - If any test fails, analyze the stack trace and fix the code.
       - **Constraint**: Do not simply change the test to make it pass. Fix the implementation.
