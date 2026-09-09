---
description: Add an EF Core migration, generate the idempotent SQL script, and verify with check-migration-drift.
argument-hint: "<Module> <MigrationName>"
allowed-tools: Read, Bash, Glob, Grep
---

Add migration: $ARGUMENTS

1. `make ef-add-{Module} name={MigrationName}`.
2. Review the generated files: unintended drops or renames, missing indexes. A change to shared EF config (for example the `AuditLog` base configuration) needs a migration in every module that shares it, or boot fails with `PendingModelChangesWarning`.
3. `make ef-script-{Module} from={PreviousMigration} to={MigrationName}` (`from=0` for a first migration). Confirm the script landed in `migrations/{Module}/` and is tracked by git.
4. `make build`.
5. `make check-migration-drift`. This is the gate, not `make build`: the deploy sidecar applies only what is committed under `migrations/`, and build cannot see that directory.

## Removing, renaming, or squashing migrations

`dotnet ef migrations remove`, hand-deleting migration files, or squashing history leaves `.sql` scripts whose MigrationId the model no longer has. `check-migration-drift` only matches ids that exist, so an orphaned script passes the check and then reapplies dead schema on a fresh DB.

1. Delete every `migrations/{Module}/*.sql` whose 14-digit MigrationId is absent from `src/Modules/{Module}/{Module}.Infrastructure/Persistence/Migrations/*.cs`.
2. Regenerate scripts for the remaining migrations in order via `ef-script-{Module}`.
3. `make check-migration-drift`: every model migration has exactly one script, no script references a missing id.
