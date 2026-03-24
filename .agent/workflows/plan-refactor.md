---
description: Plan a codebase refactoring
---
# Workflow: Plan Refactor

This workflow outlines the exact steps an agent should take when tasked with refactoring existing code to align with architectural standards or improve performance.

1. **Review Context**: Read the user's refactoring goal and target scope (module, class, or pattern).
2. **Load Constitution**: Read `.agent/rules/00-context-map.md` and `.agent/rules/01-instructions.md` to load the Code Constitution into the active context.
3. **Analyze Target**: Inspect the current implementation of the target code. Identify specific violations of the constitution (e.g., cross-module coupling, missing `.AsNoTracking()`, imperative logic that should be functional).
4. **Generate Output**: Create a `refactor_plan.md` artifact detailing the specific files to be modified, the code smells being addressed, and the expected structural changes.
5. **Seek Approval**: Wait for User approval via `notify_user` before proceeding to execution.
