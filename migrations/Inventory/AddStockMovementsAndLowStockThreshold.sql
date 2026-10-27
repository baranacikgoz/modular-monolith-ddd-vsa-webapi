START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027080221_AddStockMovementsAndLowStockThreshold') THEN
    ALTER TABLE "Inventory"."StockLevels" ADD "LowStockThreshold" integer NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027080221_AddStockMovementsAndLowStockThreshold') THEN
    CREATE TABLE "Inventory"."StockMovements" (
        "Id" uuid NOT NULL,
        "StockLevelId" uuid NOT NULL,
        "Type" character varying(32) NOT NULL,
        "Quantity" integer NOT NULL,
        "QuantityOnHandAfter" integer NOT NULL,
        "Reason" character varying(256),
        "ReservationId" uuid,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_StockMovements" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_StockMovements_StockLevels_StockLevelId" FOREIGN KEY ("StockLevelId") REFERENCES "Inventory"."StockLevels" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027080221_AddStockMovementsAndLowStockThreshold') THEN
    CREATE INDEX "IX_StockMovements_StockLevelId_CreatedOn" ON "Inventory"."StockMovements" ("StockLevelId", "CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027080221_AddStockMovementsAndLowStockThreshold') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027080221_AddStockMovementsAndLowStockThreshold', '10.0.14');
    END IF;
END $EF$;
COMMIT;

