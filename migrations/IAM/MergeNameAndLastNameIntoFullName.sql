START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    DROP INDEX "IAM"."IX_Users_SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" DROP COLUMN "SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" ADD "FullName" character varying(100) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" ADD COLUMN "SearchVector" tsvector NULL GENERATED ALWAYS AS (to_tsvector('english'::regconfig, COALESCE("FullName", ''))) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    CREATE INDEX "IX_Users_SearchVector" ON "IAM"."Users" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    DROP INDEX "IAM"."IX_Users_NationalIdentityNumber";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" DROP COLUMN "NationalIdentityNumber";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" DROP COLUMN "LastName";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    ALTER TABLE "IAM"."Users" DROP COLUMN "Name";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511160325_MergeNameAndLastNameIntoFullName') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260511160325_MergeNameAndLastNameIntoFullName', '10.0.7');
    END IF;
END $EF$;
COMMIT;

