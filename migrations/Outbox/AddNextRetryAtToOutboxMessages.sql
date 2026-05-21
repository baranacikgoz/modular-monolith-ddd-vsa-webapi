START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260521130048_AddNextRetryAtToOutboxMessages') THEN
    DROP INDEX "Outbox"."IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260521130048_AddNextRetryAtToOutboxMessages') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ADD "NextRetryAt" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260521130048_AddNextRetryAtToOutboxMessages') THEN
    CREATE INDEX "IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn" ON "Outbox"."OutboxMessages" ("IsProcessed", "FailedOn", "NextRetryAt", "CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260521130048_AddNextRetryAtToOutboxMessages') THEN
    INSERT INTO "Outbox"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260521130048_AddNextRetryAtToOutboxMessages', '10.0.7');
    END IF;
END $EF$;
COMMIT;

