skill:
  name: "fix-bug"
  description: "A rigorous workflow to reproduce, diagnose, and fix a bug without guessing."
  inputs:
    - name: bug_description
      description: "What is happening vs what should happen."
    - name: module
      description: "The suspected module."
    - name: logs_or_traces
      description: "Error messages, stack traces, or OTel Trace IDs."

  instructions: |
    1. **Initial Analysis**:
       - Search the codebase for keywords in the `logs_or_traces`.
       - Identify the specific Endpoint, Aggregate, or Service involved.

    2. **Phase 1: Reproduction (The Red Test)**:
       - **Constraint**: Do NOT modify production code yet.
       - Create a new test file: `src/Modules/{{module}}/Tests/Bugs/Issue_[Timestamp].cs`.
       - Use the `Bogus` library to recreate the exact state described in the `bug_description`.
       - Write a test that asserts the **Expected** behavior.
       - Run `dotnet test`. **The test must FAIL.**
       - If the test passes, the bug is not reproduced. Inform the user and ask for more context.

    3. **Phase 2: Diagnosis**:
       - Once the test is failing, use the test execution to step through the logic.
       - Check:
         - Invariant violations in the Domain Aggregate.
         - EF Core projection errors (missing `Include` or mapping logic).
         - Messaging race conditions in the Outbox.

    4. **Phase 3: The Fix (The Green Move)**:
       - Apply the minimal code change necessary to fix the bug.
       - **Constitution Check**: Ensure the fix doesn't violate rules (e.g., don't remove `sealed`, don't add `AutoMapper`).
       - Run the reproduction test again. **The test must PASS.**

    5. **Phase 4: Regression & Cleanup**:
       - Run `run-quality-gate` for the entire module.
       - Move the reproduction test into the permanent test suite (e.g., `src/Modules/{{module}}/Tests/Endpoints/...`) to prevent future regressions.
       - Delete the temporary `Bugs/Issue_[Timestamp].cs` file.

    6. **Report**: Summarize the Root Cause and the Fix applied.
