START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027081002_AddStoreDailyStockTotals') THEN
    CREATE TABLE "Inventory"."StoreDailyStockTotals" (
        "StoreId" uuid NOT NULL,
        "Day" date NOT NULL,
        "CommittedReservations" integer NOT NULL,
        "CommittedUnits" bigint NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_StoreDailyStockTotals" PRIMARY KEY ("StoreId", "Day")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027081002_AddStoreDailyStockTotals') THEN
    CREATE UNIQUE INDEX "IX_StockMovements_ReservationId" ON "Inventory"."StockMovements" ("ReservationId") WHERE "Type" = 'ReservationCommitted';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027081002_AddStoreDailyStockTotals') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027081002_AddStoreDailyStockTotals', '10.0.14');
    END IF;
END $EF$;
COMMIT;

