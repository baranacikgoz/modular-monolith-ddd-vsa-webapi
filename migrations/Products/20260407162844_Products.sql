START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "Products"."EventStoreEvents" RENAME TO "AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "Products"."AuditLog" RENAME CONSTRAINT "PK_EventStoreEvents" TO "PK_AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    DROP INDEX "Products"."IX_EventStoreEvents_AggregateType";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    CREATE INDEX "IX_AuditLog_AggregateId_AggregateType_CreatedOn" ON "Products"."AuditLog" ("AggregateId", "AggregateType", "CreatedOn" DESC);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260407125159_RenameEventStoreEventsToAuditLog', '10.0.1');
    END IF;
END $EF$;
COMMIT;

