---
description: Plan a new architectural feature
---

# Workflow: Plan Feature

This workflow outlines the exact steps an agent should take when tasked with planning a new feature.

1. **Review Context**: Read the user's high-level feature description.
2. **Load Constitution**: Read `.agent/rules/00-context-map.md` and `.agent/rules/01-instructions.md` to load the Code Constitution into the active context.
3. **Discover Knowledge**: Search the `knowledge_discovery` subsystem (KIs) to check if an identical or similar feature pattern has already been solved and documented.
4. **Identify Boundaries**: Determine exactly which Modular Monolith boundary this feature belongs to. If it spans multiple modules, plan the `IntegrationEvents` necessary for async communication.
5. **Generate Output**: Detail all files to be modified, data structures, and the testing strategy. The complete plan MUST be placed in the Pull Request description.