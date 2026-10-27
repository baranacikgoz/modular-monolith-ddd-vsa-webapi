START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095930_AddStockBulkAdjustJobRequeueCount') THEN
    ALTER TABLE "Inventory"."StockBulkAdjustJobs" ADD "RequeueCount" integer NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095930_AddStockBulkAdjustJobRequeueCount') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027095930_AddStockBulkAdjustJobRequeueCount', '10.0.14');
    END IF;
END $EF$;
COMMIT;

