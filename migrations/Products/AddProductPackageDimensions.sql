START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027091534_AddProductPackageDimensions') THEN
    ALTER TABLE "Products"."Products" ADD "PackageHeightCm" numeric(10,2);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027091534_AddProductPackageDimensions') THEN
    ALTER TABLE "Products"."Products" ADD "PackageLengthCm" numeric(10,2);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027091534_AddProductPackageDimensions') THEN
    ALTER TABLE "Products"."Products" ADD "PackageWeightKg" numeric(10,3);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027091534_AddProductPackageDimensions') THEN
    ALTER TABLE "Products"."Products" ADD "PackageWidthCm" numeric(10,2);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027091534_AddProductPackageDimensions') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027091534_AddProductPackageDimensions', '10.0.14');
    END IF;
END $EF$;
COMMIT;

