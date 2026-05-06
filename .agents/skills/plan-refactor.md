---
description: Plan a codebase refactoring — identify violations, list file changes, assess risk.
---

Plan a codebase refactoring. Produce a structured plan before touching any code.

1. **Understand the goal**: clarify target scope (module, class, or pattern) and what architectural violation or quality issue is being addressed.

2. **Analyze current state**: inspect the target code. Identify specific violations such as:
   - Cross-module coupling in `.csproj` references
   - Missing `.AsNoTracking()` on read queries
   - Imperative `if/else` blocks that should be functional pipelines
   - AutoMapper or other mapping library usage
   - Magic string localization keys instead of `IResxLocalizer`
   - Controllers instead of Minimal API endpoints
   - Direct Kafka/bus publishing instead of `RaiseEvent`

3. **File inventory**: list every file to be modified, what the current code looks like, and what it will look like after.

4. **Risk assessment**: flag any changes that could break the Outbox flow, CDC pipeline, or cross-module communication.

5. **Output**: write the complete plan as a structured markdown document. Implementation begins only after this plan is approved.
