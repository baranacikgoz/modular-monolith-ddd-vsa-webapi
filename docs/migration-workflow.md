# Database Migration Workflow

## Overview

We do **not** auto-apply migrations at startup. All schema changes go through DBA review.

> **The application will refuse to start** if pending migrations are detected.
> Test environments (Testcontainers) are exempt — they auto-migrate via the `IAutoMigrateMarker` DI seam.

---

## Steps

### 1. Create a Migration
```bash
make ef-add-Products name=AddPriceColumn
```

### 2. Generate an Idempotent SQL Script
```bash
make ef-script-Products
```
The script is saved to `migrations/Products/<timestamp>_Products.sql`.

### 3. Commit Both Artifacts
- The migration `.cs` files under `src/Modules/<Module>/<Module>.Infrastructure/Persistence/Migrations/`
- The generated `.sql` file under `migrations/<Module>/`

### 4. DBA Review & Execution
- The DBA reviews the `.sql` script in the PR.
- The DBA may adjust (e.g. add `CONCURRENTLY` to index creation, schedule during maintenance window).
- The DBA executes the script against the target database.

### 5. Deploy
```bash
# Standard deployment — app will verify on startup
```
On startup, `MigrationGuard` checks for pending migrations. If any remain → the app throws and refuses to start with a clear message:
```
Module 'Products' has 1 pending migration(s): [20260403_AddPriceColumn].
Generate the SQL script with 'make ef-script-Products' and have a DBA apply it before deploying.
```

---

## Module Commands Reference

| Module   | Add Migration                          | Generate Script             |
|----------|----------------------------------------|-----------------------------|
| IAM      | `make ef-add-IAM name=<Name>`         | `make ef-script-IAM`        |
| Products | `make ef-add-Products name=<Name>`    | `make ef-script-Products`   |
| Outbox   | `make ef-add-Outbox name=<Name>`      | `make ef-script-Outbox`     |
| All      | —                                      | `make ef-script-all`        |

---

## Architecture Notes

### Seeding
Database seeders (`IDatabaseSeeder`) run automatically after every deployment via `DatabaseSeederOrchestrator` (a `BackgroundService`). They are **idempotent** — they skip data that already exists. Seeders run in module priority order:
1. IAM (priority 2) — creates roles and seed users
2. Products (priority 4) — creates stores and products using seed user IDs from IAM

### Test Environments
Integration tests register `IAutoMigrateMarker` in DI. `MigrationGuard` detects this marker and auto-applies migrations against the disposable Testcontainers Postgres instance. No DBA is involved.
