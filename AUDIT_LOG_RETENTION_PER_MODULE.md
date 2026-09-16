# Problem: AuditLog retention is one global value, but modules need different horizons

## Current behavior

`AuditLogOptions` (`src/Common/Common.Application/Options/AuditLogOptions.cs`) exposes exactly two knobs,
`RetentionDays` and `PurgeBatchSize`, bound from a single configuration section. `AuditLogRetentionService`
(`src/Common/Common.Infrastructure/Persistence/AuditLog/AuditLogRetentionService.cs`) runs on a schedule,
discovers every schema in the database that has an `AuditLog` table (`information_schema.tables` query), and
deletes rows older than `cutoffDate = UtcNow - RetentionDays` from every one of them, in the same pass, with the
same cutoff. There is no per-schema or per-module override anywhere in the retention path.

## Why this is wrong

`AuditLog` is also the durable record for any module that chooses to represent a "who, when, before, after"
history through a domain event rather than a bespoke table (a legitimate and encouraged pattern: the event
payload plus `CreatedBy`/`CreatedOn` already cover the requirement, so there is no reason to duplicate that data
in a second table). That choice is sound only if the record actually survives as long as the business needs it
to. Different modules have wildly different needs for the same table:

- A technical or diagnostic trail (a webhook delivery attempt, a cache-warm event) can reasonably expire in
  weeks.
- A financial ledger or a stock-correction ledger needs to survive for years, sometimes for the life of the
  business, for audit and dispute-resolution reasons.

One global `RetentionDays` cannot serve both. Raising it globally to satisfy the long-lived case means every
module's rows, including the ones that never needed to survive that long, grow the table indefinitely. Lowering
it to keep the table small means the long-lived case loses data it needed.

## Ask

Make retention configurable per schema (or per module), with the current single value staying as the default
for any module that does not override it. A plausible shape, without prescribing the implementation:

```json
{
  "AuditLogOptions": {
    "RetentionDays": 90,
    "PurgeBatchSize": 5000,
    "PerSchemaOverrides": {
      "Orders": 3650
    }
  }
}
```

`AuditLogRetentionService` already iterates schemas one at a time to build its per-schema `DELETE`; it has
everything it needs to look up an override by schema name at that point in the loop and fall back to the
top-level value when none is configured. No public contract of `AuditLogOptions` needs to disappear, so this is
additive.
