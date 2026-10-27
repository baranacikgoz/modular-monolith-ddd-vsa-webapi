START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027083525_AddWarehouseConnections') THEN
    CREATE TABLE "Inventory"."WarehouseConnections" (
        "Id" uuid NOT NULL,
        "StoreId" uuid NOT NULL,
        "Provider" character varying(32) NOT NULL,
        "BaseUrl" character varying(512) NOT NULL,
        "Status" character varying(32) NOT NULL,
        "EncryptedApiCredential" bytea NOT NULL,
        "EncryptedWebhookSecret" bytea NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Version" bigint NOT NULL,
        CONSTRAINT "PK_WarehouseConnections" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027083525_AddWarehouseConnections') THEN
    CREATE UNIQUE INDEX "IX_WarehouseConnections_StoreId" ON "Inventory"."WarehouseConnections" ("StoreId") WHERE "Status" = 'Active';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027083525_AddWarehouseConnections') THEN
    CREATE INDEX "IX_WarehouseConnections_StoreId_CreatedOn" ON "Inventory"."WarehouseConnections" ("StoreId", "CreatedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027083525_AddWarehouseConnections') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027083525_AddWarehouseConnections', '10.0.14');
    END IF;
END $EF$;
COMMIT;

