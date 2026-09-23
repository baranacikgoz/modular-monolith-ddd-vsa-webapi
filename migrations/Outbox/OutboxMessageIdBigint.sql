START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923230522_OutboxMessageIdBigint') THEN
    DROP INDEX "Outbox"."IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923230522_OutboxMessageIdBigint') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ALTER COLUMN "Id" TYPE bigint;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923230522_OutboxMessageIdBigint') THEN
    CREATE INDEX "IX_OutboxMessages_NextRetryAt_CreatedOn" ON "Outbox"."OutboxMessages" ("NextRetryAt", "CreatedOn") WHERE "IsProcessed" = false AND "FailedOn" IS NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923230522_OutboxMessageIdBigint') THEN
    INSERT INTO "Outbox"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260923230522_OutboxMessageIdBigint', '10.0.11');
    END IF;
END $EF$;
COMMIT;

