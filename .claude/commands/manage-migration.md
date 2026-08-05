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
   - If the change touches shared EF config (e.g. a base-class `AuditLog` configuration), a matching migration is needed in every module sharing that config, not just this one, otherwise `make build` fails with `PendingModelChangesWarning` on boot.

3. **Generate idempotent SQL script** (both `from` and `to` are required, `to` also names the output file):
   ```bash
   make ef-script-{Module} from={PreviousMigrationName} to={TargetMigrationName}
   ```

4. **Verify**: confirm the script was written to `migrations/{Module}/` and is tracked by git.

5. `make build`, confirm zero warnings.

6. **`make check-migration-drift`, mandatory, every time, no exceptions.** Run this as the actual
   final step, not `make build`. Build cannot see the SQL directory; only this check does. A green
   `make build` with a missing or stale `.sql` file still ships a broken deploy, the sidecar
   applies whatever is committed under `migrations/`, nothing more. This is the step that keeps
   getting skipped in practice, so treat it as the one that actually gates "done."

## Removing, renaming, or squashing an existing migration

`make ef-add-*` is not the only way a migration changes. Any of these leave the model with a
**different** MigrationId than whatever `.sql` is currently committed, and CI won't catch it until
push because the stale script still "exists" (`check-migration-drift` only fails once it's run):

- `dotnet ef migrations remove` (undo the last unapplied migration)
- Deleting a migration's three files by hand and re-running `ef-add` under the same or a new name
- Squashing N incremental migrations into one (e.g. collapsing WIP history into a clean `Initial`
  before a merge): the old per-migration `.sql` files stay committed under MigrationIds the model
  no longer has, and the new squashed migration may never get a script at all. A fresh-DB deploy
  would run the orphaned scripts (they still "match" the drift check, since it only looks for a
  matching ID, not whether that ID is still in the model) before the new one, and either
  duplicate-create tables or corrupt `__EFMigrationsHistory` with IDs the model never produced.

When you do any of the above:

1. **Delete every `.sql` file under `migrations/{Module}/` whose embedded MigrationId no longer
   appears in `src/Modules/{Module}/{Module}.Infrastructure/Persistence/Migrations/*.cs`** (grep the
   14-digit ID from the filename/contents against the `.cs` files present). Do not leave an old
   script "just in case", an orphaned script that still matches a name is more dangerous than a
   missing one, because `check-migration-drift` will call it "ok" while it silently reapplies dead
   schema state.
2. Regenerate the script(s) for whatever migrations remain, in order, via `ef-script-{Module}`
   (`from=0` for a squashed-to-single `Initial`).
3. Run `make check-migration-drift` and confirm every remaining model migration has exactly one
   matching script, and no committed script references an ID absent from the model.
