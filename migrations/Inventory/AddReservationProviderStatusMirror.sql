START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027105145_AddReservationProviderStatusMirror') THEN
    ALTER TABLE "Inventory"."StockReservations" ADD "IsReadOnly" boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027105145_AddReservationProviderStatusMirror') THEN
    ALTER TABLE "Inventory"."StockReservations" ADD "ProviderStatus" integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027105145_AddReservationProviderStatusMirror') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027105145_AddReservationProviderStatusMirror', '10.0.14');
    END IF;
END $EF$;
COMMIT;

