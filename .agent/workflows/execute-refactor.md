---
description: Execute a planned refactoring
---
# Workflow: Execute Refactor

This workflow handles the actual implementation of a planned refactoring, ensuring zero regressions and strict architectural compliance.

1. **Review Plan**: Read the approved `refactor_plan.md` artifact to understand the exact scope of the changes.
2. **Pre-Flight Check**: Before changing any business logic, run `make test-[module]` (e.g., `make test-iam`) on the target module to ensure the existing tests provide a green baseline. If there are no tests for the legacy code, warn the user.
3. **Execute Refactoring**: Apply the changes incrementally to the target files. Refactor imperative logic to functional pipelines, remove mapping libraries, or enforce strict modular boundaries as defined in the plan.
4. **Post-Flight Verification**: 
    - Re-run `make test-[module]` (or `make test` for full suite) on the affected module(s) to guarantee no regressions (Green state).
    - Audit `.csproj` files to ensure no illegal boundary references were introduced.
5. **Handoff**: Use `notify_user` to return execution control back to the Developer, including a summary of the changes and the successful test output.
