---
description: Diff and reconcile CLAUDE.md vs GEMINI.md and .claude/commands/ vs .agents/skills/ to keep both AI toolchains identical.
argument-hint: ""
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Sync AI toolchain settings.

1. **Compare root instruction files**: diff `CLAUDE.md` against `GEMINI.md` section by section. Report any rules, examples, or module inventory present in one but missing or different in the other.

2. **Compare command/skill sets**:
   ```bash
   diff <(ls .claude/commands/) <(ls .agents/skills/)
   ```
   Report any file present in one directory but missing in the other.

3. **Compare matched pairs**: for each command/skill pair with the same name, diff their content (ignoring YAML frontmatter that exists only in `.agents/skills/`). Report substantive differences.

4. **Apply fixes**: update the lagging file to match the leading one. Preserve:
   - `.claude/commands/` format: YAML frontmatter with `description`, `argument-hint`, `allowed-tools`, `context`, `model`
   - `.agents/skills/` format: same frontmatter plus `description` key for Antigravity's skill picker

5. **Touch GEMINI.md** after any sync to reset the sync-reminder hook:
   ```bash
   touch GEMINI.md
   ```

6. **Report**: every file updated and a one-line summary of what changed.
