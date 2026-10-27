START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027140746_AddReservationReleaseAttemptTime') THEN
    ALTER TABLE "Inventory"."StockReservations" ADD "LastReleaseAttemptOn" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027140746_AddReservationReleaseAttemptTime') THEN
    CREATE INDEX "IX_StockReservations_LastReleaseAttemptOn" ON "Inventory"."StockReservations" ("LastReleaseAttemptOn") WHERE "LastReleaseAttemptReference" IS NOT NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027140746_AddReservationReleaseAttemptTime') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027140746_AddReservationReleaseAttemptTime', '10.0.14');
    END IF;
END $EF$;
COMMIT;

