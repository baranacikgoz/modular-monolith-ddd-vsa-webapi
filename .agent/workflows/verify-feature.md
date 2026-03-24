---
description: Verify feature correctness and run quality gate
---
# Workflow: Verify Feature

This workflow runs the final checks on implemented features, verifying them against the test suite and architectural rules.

// turbo-all
1. **Execute Tests**: Run `dotnet test src/Modules/[Module]/[Module].Tests` until green exit code 0 is achieved. Fix any minor compilation logic bugs preventing success.
2. **Audit Boundaries**: Inspect the module's `.csproj` history to ensure NO cross-module references were accidentally added during implementation.
3. **Generate Audit**: Create or update the `walkthrough.md` artifact containing a summary of changes, the Diff history, and the console output of the successful test run.
4. **Handoff**: Use `notify_user` to return execution control back to the Developer indicating the workflow is 100% complete and verified.
