---
description: Add an EF Core migration and generate the idempotent SQL script for a specific module.
---

Add an EF Core migration and generate the idempotent SQL script. Ask for module name and migration name if not provided.

1. **Add migration** via Makefile:
   ```bash
   make ef-add-{Module} name={MigrationName}
   ```

2. **Review** the generated migration files. Check for:
   - Unintended column drops or renames
   - Missing index additions/removals
   - Any schema change that could break running consumers or Debezium connectors

3. **Generate idempotent SQL script**:
   ```bash
   make ef-script-{Module} from={PreviousMigrationName}
   ```
   Leave `from` empty to generate from the beginning.

4. **Verify output**: confirm the script was written to `migrations/{Module}/` and is tracked by version control.

5. Run `make build` to confirm the project still compiles cleanly after the migration was added.
