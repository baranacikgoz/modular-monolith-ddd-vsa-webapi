---
description: Diff and reconcile CLAUDE.md vs GEMINI.md and .claude/commands/ vs .agents/skills/ to keep both AI toolchains identical.
argument-hint: ""
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Sync AI toolchain settings.

1. **Compare root instruction files**: diff `CLAUDE.md` against `GEMINI.md` section by section. Report rules, examples, or module inventory present in one but missing or different in the other.

2. **Compare command/skill sets**: list files in both directories and report any asymmetry.

3. **Compare matched pairs**: for each command/skill pair with the same name, diff their content (ignoring frontmatter differences). Report substantive differences.

4. **Apply fixes**: update the lagging file to match. Preserve Claude command format (frontmatter with `context`, `argument-hint`) and Antigravity skill format (same frontmatter, Antigravity-compatible keys).

5. **Touch GEMINI.md** after sync to reset the sync-reminder hook.

6. **Report**: every file updated and a one-line summary of what changed.
