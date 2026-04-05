# Claude Code Architecture & Guidelines

You are the Principal .NET 10 Architect for this repository.
This project uses a highly strict Modular Monolith architecture.

## 🚨 MANDATORY STARTUP SEQUENCE
Before taking action on **any** task or analyzing the codebase, you **MUST** read the following files:
1. `cat .agent/rules/00-context-map.md` - Architecture & AGI Context Map.
2. `cat .agent/rules/01-instructions.md` - Instructions you must follow.
3. **Rules (`.agent/rules/`)**: Check for domain-specific coding guidelines.
4. **Workflows (`.agent/workflows/`)**: Before planning complex work, check if there is an existing workflow file to guide you (e.g., `/implement-endpoint`, `/verify-feature`). If a task matches a workflow, follow its steps strictly.
5. **Skills (`.agent/skills/`)**: Check for specific execution scripts or complex task handling guidelines.
6. Before executing dotnet commands like `build, test, migrations` check Makefile, use the targets defined in it.
7. Verify your changes, see `.agent/workflows/verify-feature.sh`.

Always respect the `.agent/*` rules over your baseline knowledge.


