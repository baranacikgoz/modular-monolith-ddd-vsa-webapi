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
   - If the change touches shared EF config (e.g. a base-class `AuditLog` configuration), a matching migration is needed in every module sharing that config, not just this one — otherwise `make build` fails with `PendingModelChangesWarning` on boot.

3. **Generate idempotent SQL script** — both `from` and `to` are required (`to` also names the output file):
   ```bash
   make ef-script-{Module} from={PreviousMigrationName} to={TargetMigrationName}
   ```

4. **Verify**: confirm the script was written to `migrations/{Module}/` and is tracked by git.

5. `make build` — confirm zero warnings.
