---
description: Plan a codebase refactoring — identify violations, list exact file changes, assess risk before touching any code.
argument-hint: "<target scope and goal>"
context: fork
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Plan refactor: $ARGUMENTS

1. **Analyze current state**: inspect the target code. Identify specific violations:
   - Cross-module coupling in `.csproj` references
   - Missing `.AsNoTracking()` on read queries
   - Imperative `if/else` blocks that should be functional pipelines
   - AutoMapper or mapping library usage
   - Magic string localization keys instead of `IResxLocalizer`
   - Controllers instead of Minimal API endpoints
   - Direct bus publishing instead of `RaiseEvent`

2. **File inventory**: list every file to be modified, what the current code looks like (brief excerpt), and what it will look like after.

3. **Risk assessment**: flag any changes that could break the Outbox flow, shipped `V{n}...DomainEvent` versioning (never edit in place — add `V{n+1}` instead), or cross-module communication (`IntegrationEvents` / `InterModuleRequests`).

4. **Output**: produce a structured markdown plan. Implementation begins only after this plan is approved.
