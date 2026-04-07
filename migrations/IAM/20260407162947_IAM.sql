START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125227_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "IAM"."EventStoreEvents" RENAME TO "AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125227_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "IAM"."AuditLog" RENAME CONSTRAINT "PK_EventStoreEvents" TO "PK_AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125227_RenameEventStoreEventsToAuditLog') THEN
    DROP INDEX "IAM"."IX_EventStoreEvents_AggregateType";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125227_RenameEventStoreEventsToAuditLog') THEN
    CREATE INDEX "IX_AuditLog_AggregateId_AggregateType_CreatedOn" ON "IAM"."AuditLog" ("AggregateId", "AggregateType", "CreatedOn" DESC);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125227_RenameEventStoreEventsToAuditLog') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260407125227_RenameEventStoreEventsToAuditLog', '10.0.1');
    END IF;
END $EF$;
COMMIT;

