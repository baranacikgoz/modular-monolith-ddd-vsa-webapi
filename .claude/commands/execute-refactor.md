---
description: Execute an approved refactor plan incrementally with zero regressions.
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Execute refactor for: $ARGUMENTS

1. Read the approved plan from the conversation.
2. Green baseline: `make test-{module}`. If the code being changed has no tests, warn the user and stop.
3. Apply the plan file by file. `make build` after each logical batch.
4. `/verify-feature {Module}`.
5. Report changed files and the test output.
