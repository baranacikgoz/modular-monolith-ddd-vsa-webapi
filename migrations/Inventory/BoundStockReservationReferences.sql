START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924105256_BoundStockReservationReferences') THEN
    ALTER TABLE "Inventory"."StockReservations" ALTER COLUMN "ProviderReference" TYPE character varying(256);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924105256_BoundStockReservationReferences') THEN
    ALTER TABLE "Inventory"."StockReservations" ALTER COLUMN "LastReleaseAttemptReference" TYPE character varying(64);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924105256_BoundStockReservationReferences') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260924105256_BoundStockReservationReferences', '10.0.11');
    END IF;
END $EF$;
COMMIT;

