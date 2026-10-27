START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090905_AddProductAttributes') THEN
    ALTER TABLE "Products"."Products" ADD "Attributes" jsonb NOT NULL DEFAULT ('[]'::jsonb);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090905_AddProductAttributes') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027090905_AddProductAttributes', '10.0.14');
    END IF;
END $EF$;
COMMIT;

