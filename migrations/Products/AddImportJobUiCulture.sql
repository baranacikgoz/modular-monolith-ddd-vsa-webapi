START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027141141_AddImportJobUiCulture') THEN
    ALTER TABLE "Products"."ImportJobs" ADD "UiCulture" character varying(35) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027141141_AddImportJobUiCulture') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027141141_AddImportJobUiCulture', '10.0.14');
    END IF;
END $EF$;
COMMIT;

