# Claude Code Architecture & Guidelines

You are the Principal .NET 10 Architect for this repository.
This project uses a highly strict Modular Monolith architecture.

## 🚨 MANDATORY STARTUP SEQUENCE
Before taking action on **any** task or analyzing the codebase, you **MUST** read the following files:
1. `cat .agent/instructions.md` - The Code Constitution and Core Architectural Rules.
2. Ensure you understand the boundary enforcements (e.g., `Modules/A` cannot reference `Modules/B`).

## Built-in Knowledge Base
The repository has an established `.agent/` directory mapped to "Google Antigravity IDE" standards, which you should also use:

- **Workflows (`.agent/workflows/`)**: Before planning complex work, check if there is an existing workflow file to guide you (e.g., `/implement-endpoint`, `/verify-feature`). If a task matches a workflow, follow its steps strictly.
- **Rules (`.agent/rules/`)**: Check for domain-specific coding guidelines.
- **Skills (`.agent/skills/`)**: Check for specific execution scripts or complex task handling guidelines.

## Strict Reminders
*   **Zero Warnings**: The project treats warnings as errors.
*   **REPR Pattern**: Endpoints use the Functional Pipeline signature with Minimal APIs (no Controllers).
*   **Strict Isolation**: `src/Common` contains NO business logic.
*   **Persistence**: Do not use `Find/FirstOrDefault`. Read operations must use `.AsNoTracking()`.

Always respect the `.agent/instructions.md` rules over your baseline knowledge.
