START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    DROP INDEX "Outbox"."IX_OutboxMessages_CreatedOn_IsProcessed";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    ALTER TABLE "Outbox"."OutboxMessages" DROP COLUMN "EventType";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ADD "FailedOn" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ADD "RetryCount" integer NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    CREATE INDEX "IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn" ON "Outbox"."OutboxMessages" ("IsProcessed", "FailedOn", "CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513125905_ReplaceKafkaWithPollingOutbox') THEN
    INSERT INTO "Outbox"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260513125905_ReplaceKafkaWithPollingOutbox', '10.0.7');
    END IF;
END $EF$;
COMMIT;

