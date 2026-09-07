DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Notifications') THEN
        CREATE SCHEMA "Notifications";
    END IF;
END $EF$;
CREATE TABLE IF NOT EXISTS "Notifications"."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Notifications') THEN
            CREATE SCHEMA "Notifications";
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE TABLE "Notifications"."AuditLog" (
        "AggregateId" uuid NOT NULL,
        "Version" bigint NOT NULL,
        "AggregateType" character varying(128) NOT NULL,
        "EventType" character varying(256) NOT NULL,
        "Event" jsonb NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_AuditLog" PRIMARY KEY ("AggregateId", "Version")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE TABLE "Notifications"."DeviceRegistrations" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "DeviceId" uuid NOT NULL,
        "ClientId" character varying(50) NOT NULL,
        "SessionId" character varying(64) NOT NULL,
        "DeviceName" character varying(100),
        "PushToken" character varying(4096),
        "PushTokenUpdatedOn" timestamp with time zone,
        "IsActive" boolean NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_DeviceRegistrations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE INDEX "IX_AuditLog_AggregateId_AggregateType_CreatedOn" ON "Notifications"."AuditLog" ("AggregateId", "AggregateType", "CreatedOn" DESC);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE INDEX "IX_AuditLog_CreatedOn" ON "Notifications"."AuditLog" ("CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE UNIQUE INDEX "IX_DeviceRegistrations_UserId_DeviceId_ClientId" ON "Notifications"."DeviceRegistrations" ("UserId", "DeviceId", "ClientId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    CREATE INDEX "IX_DeviceRegistrations_UserId_SessionId" ON "Notifications"."DeviceRegistrations" ("UserId", "SessionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20260904165228_InitialDeviceRegistrations') THEN
    INSERT INTO "Notifications"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260904165228_InitialDeviceRegistrations', '10.0.11');
    END IF;
END $EF$;
COMMIT;

