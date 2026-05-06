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

2. **Review** the generated migration files for unintended column drops, renames, or schema changes that could break Debezium connectors.

3. **Generate idempotent SQL script**:
   ```bash
   make ef-script-{Module} from={PreviousMigrationName}
   ```

4. **Verify**: confirm the script was written to `migrations/{Module}/` and is tracked by git.

5. `make build` — confirm zero warnings.
