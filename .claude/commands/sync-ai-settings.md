Diff and reconcile the two AI toolchain configurations to ensure they are behaviourally identical.

1. **Compare root instruction files**: diff `CLAUDE.md` against `GEMINI.md` section by section. Report any rules, examples, or module inventory present in one but missing or different in the other.

2. **Compare command/skill sets**:
   - List files in `.claude/commands/`
   - List files in `.agents/skills/`
   - Report any file present in one directory but missing in the other.

3. **Compare content of matched pairs**: for each command/skill pair with the same name, diff their content (ignoring the YAML frontmatter that only exists in `.agents/skills/`). Report substantive differences.

4. **Apply fixes**: for every divergence found, update the lagging file to match the leading one. Preserve Claude-specific formatting in `.claude/commands/` (no frontmatter) and Antigravity-specific formatting in `.agents/skills/` (YAML frontmatter with `description:`).

5. **Touch GEMINI.md** after any sync so the `sync-reminder` hook doesn't fire spuriously:
   ```bash
   touch GEMINI.md
   ```

6. **Report**: list every file that was updated and a one-line summary of what changed.
