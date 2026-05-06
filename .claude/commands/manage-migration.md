---
description: Add an EF Core migration via Makefile and generate the idempotent SQL script.
argument-hint: "<Module> <MigrationName>"
allowed-tools: Read, Bash, Glob, Grep
---

Add migration: $ARGUMENTS

1. **Add migration** via Makefile:
   ```bash
   make ef-add-{Module} name={MigrationName}
   ```

2. **Review** the generated migration files. Check for:
   - Unintended column drops or renames
   - Missing index changes
   - Schema changes that could break running Debezium connectors

3. **Generate idempotent SQL script**:
   ```bash
   make ef-script-{Module} from={PreviousMigrationName}
   ```
   Leave `from` empty to generate from the beginning.

4. **Verify**: confirm the script was written to `migrations/{Module}/` and is tracked by git.

5. `make build` — confirm zero warnings.
