START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027093227_AddStockPushDirtyItems') THEN
    CREATE SEQUENCE "Inventory"."StockPushMarkVersions" START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027093227_AddStockPushDirtyItems') THEN
    CREATE TABLE "Inventory"."StockPushDirtyItems" (
        "StockLevelId" uuid NOT NULL,
        "StoreId" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "MarkVersion" bigint NOT NULL,
        "MarkedOn" timestamp with time zone NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_StockPushDirtyItems" PRIMARY KEY ("StockLevelId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027093227_AddStockPushDirtyItems') THEN
    CREATE INDEX "IX_StockPushDirtyItems_MarkedOn" ON "Inventory"."StockPushDirtyItems" ("MarkedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027093227_AddStockPushDirtyItems') THEN
    CREATE INDEX "IX_StockPushDirtyItems_StoreId_MarkedOn" ON "Inventory"."StockPushDirtyItems" ("StoreId", "MarkedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027093227_AddStockPushDirtyItems') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027093227_AddStockPushDirtyItems', '10.0.14');
    END IF;
END $EF$;
COMMIT;

