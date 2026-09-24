START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Products') THEN
            CREATE SCHEMA "Products";
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE EXTENSION IF NOT EXISTS pg_trgm;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_Stores_Address" ON "Products"."Stores" USING gin ("Address" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_Stores_Name" ON "Products"."Stores" USING gin ("Name" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_ProductTemplates_Brand" ON "Products"."ProductTemplates" USING gin ("Brand" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_ProductTemplates_Color" ON "Products"."ProductTemplates" USING gin ("Color" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_ProductTemplates_Model" ON "Products"."ProductTemplates" USING gin ("Model" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    CREATE INDEX "IX_Products_Name" ON "Products"."Products" USING gin ("Name" gin_trgm_ops);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260924064640_AddTrigramIndexes') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260924064640_AddTrigramIndexes', '10.0.11');
    END IF;
END $EF$;
COMMIT;

