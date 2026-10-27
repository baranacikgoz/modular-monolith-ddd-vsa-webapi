START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027135957_AddReservationProviderEventTime') THEN
    ALTER TABLE "Inventory"."StockReservations" ADD "LastProviderEventOn" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027135957_AddReservationProviderEventTime') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027135957_AddReservationProviderEventTime', '10.0.14');
    END IF;
END $EF$;
COMMIT;

