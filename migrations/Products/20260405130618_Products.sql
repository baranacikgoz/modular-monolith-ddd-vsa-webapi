START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."Stores" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Name" || ' ' || "Description" || ' ' || "Address")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."ProductTemplates" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Brand" || ' ' || "Model" || ' ' || "Color")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."Products" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Name" || ' ' || "Description")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_Stores_SearchVector" ON "Products"."Stores" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_ProductTemplates_SearchVector" ON "Products"."ProductTemplates" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_Products_SearchVector" ON "Products"."Products" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260405100359_AddFullSearch', '10.0.1');
    END IF;
END $EF$;
COMMIT;

