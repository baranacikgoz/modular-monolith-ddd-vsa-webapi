DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Inventory') THEN
        CREATE SCHEMA "Inventory";
    END IF;
END $EF$;
CREATE TABLE IF NOT EXISTS "Inventory"."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Inventory') THEN
            CREATE SCHEMA "Inventory";
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE TABLE "Inventory"."AuditLog" (
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
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE TABLE "Inventory"."StockLevels" (
        "Id" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "QuantityOnHand" integer NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Version" bigint NOT NULL,
        CONSTRAINT "PK_StockLevels" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE TABLE "Inventory"."StockReservations" (
        "Id" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        "Status" integer NOT NULL,
        "ReservationDeadline" timestamp with time zone NOT NULL,
        "ProviderReference" text,
        "LastReleaseAttemptReference" text,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Version" bigint NOT NULL,
        CONSTRAINT "PK_StockReservations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE INDEX "IX_AuditLog_AggregateId_AggregateType_CreatedOn" ON "Inventory"."AuditLog" ("AggregateId", "AggregateType", "CreatedOn" DESC);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE INDEX "IX_AuditLog_CreatedOn" ON "Inventory"."AuditLog" ("CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_StockLevels_ProductId" ON "Inventory"."StockLevels" ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    CREATE INDEX "IX_StockReservations_ProductId_Status" ON "Inventory"."StockReservations" ("ProductId", "Status");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20260907234916_InitialCreate') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260907234916_InitialCreate', '10.0.11');
    END IF;
END $EF$;
COMMIT;

