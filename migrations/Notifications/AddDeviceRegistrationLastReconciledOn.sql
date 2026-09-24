START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064635_AddDeviceRegistrationLastReconciledOn') THEN
    ALTER TABLE "Notifications"."DeviceRegistrations" ADD "LastReconciledOn" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064635_AddDeviceRegistrationLastReconciledOn') THEN
    CREATE INDEX "IX_DeviceRegistrations_IsActive_LastReconciledOn" ON "Notifications"."DeviceRegistrations" ("IsActive", "LastReconciledOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064635_AddDeviceRegistrationLastReconciledOn') THEN
    INSERT INTO "Notifications"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260924064635_AddDeviceRegistrationLastReconciledOn', '10.0.11');
    END IF;
END $EF$;
COMMIT;

