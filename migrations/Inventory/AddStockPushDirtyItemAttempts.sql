START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027141856_AddStockPushDirtyItemAttempts') THEN
    ALTER TABLE "Inventory"."StockPushDirtyItems" ADD "LastErrorKey" character varying(128);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027141856_AddStockPushDirtyItemAttempts') THEN
    ALTER TABLE "Inventory"."StockPushDirtyItems" ADD "PushAttempts" integer NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027141856_AddStockPushDirtyItemAttempts') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027141856_AddStockPushDirtyItemAttempts', '10.0.14');
    END IF;
END $EF$;
COMMIT;

