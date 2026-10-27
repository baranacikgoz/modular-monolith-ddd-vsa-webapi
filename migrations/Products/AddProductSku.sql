START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090420_AddProductSku') THEN
    ALTER TABLE "Products"."Products" ADD "Sku" character varying(64);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090420_AddProductSku') THEN
    CREATE UNIQUE INDEX "IX_Products_StoreId_Sku" ON "Products"."Products" ("StoreId", "Sku") WHERE "Sku" IS NOT NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090420_AddProductSku') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027090420_AddProductSku', '10.0.14');
    END IF;
END $EF$;
COMMIT;

