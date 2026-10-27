START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027110754_AddStoreAccessProjection') THEN
    CREATE TABLE "Inventory"."StoreAccess" (
        "StoreId" uuid NOT NULL,
        "OwnerId" uuid NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "SourceVersion" bigint NOT NULL,
        CONSTRAINT "PK_StoreAccess" PRIMARY KEY ("StoreId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027110754_AddStoreAccessProjection') THEN
    CREATE INDEX "IX_StoreAccess_OwnerId_IsActive" ON "Inventory"."StoreAccess" ("OwnerId", "IsActive");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027110754_AddStoreAccessProjection') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027110754_AddStoreAccessProjection', '10.0.14');
    END IF;
END $EF$;
COMMIT;

