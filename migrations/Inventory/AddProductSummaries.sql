START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075227_AddProductSummaries') THEN
    CREATE TABLE "Inventory"."ProductSummaries" (
        "ProductId" uuid NOT NULL,
        "StoreId" uuid NOT NULL,
        "Name" character varying(256) NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "SourceVersion" bigint NOT NULL,
        CONSTRAINT "PK_ProductSummaries" PRIMARY KEY ("ProductId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075227_AddProductSummaries') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027075227_AddProductSummaries', '10.0.14');
    END IF;
END $EF$;
COMMIT;

