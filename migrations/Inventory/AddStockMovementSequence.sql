START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    DROP INDEX "Inventory"."IX_StockMovements_StockLevelId_CreatedOn";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    ALTER TABLE "Inventory"."StockMovements" ADD "Sequence" bigint NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    ALTER TABLE "Inventory"."StockLevels" ADD "LastMovementSequence" bigint NOT NULL DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    UPDATE "Inventory"."StockMovements" AS movement
    SET "Sequence" = numbered."Position"
    FROM (
        SELECT "Id", ROW_NUMBER() OVER (PARTITION BY "StockLevelId" ORDER BY "CreatedOn", "Id") AS "Position"
        FROM "Inventory"."StockMovements"
    ) AS numbered
    WHERE movement."Id" = numbered."Id";

    UPDATE "Inventory"."StockLevels" AS stock
    SET "LastMovementSequence" = counted."Total"
    FROM (
        SELECT "StockLevelId", COUNT(*) AS "Total"
        FROM "Inventory"."StockMovements"
        GROUP BY "StockLevelId"
    ) AS counted
    WHERE stock."Id" = counted."StockLevelId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    CREATE UNIQUE INDEX "IX_StockMovements_StockLevelId_Sequence" ON "Inventory"."StockMovements" ("StockLevelId", "Sequence");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027151331_AddStockMovementSequence') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027151331_AddStockMovementSequence', '10.0.14');
    END IF;
END $EF$;
COMMIT;

