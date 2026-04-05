START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405140955_AddFullTextSearch') THEN
    ALTER TABLE "IAM"."Users" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Name" || ' ' || "LastName")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405140955_AddFullTextSearch') THEN
    CREATE INDEX "IX_Users_SearchVector" ON "IAM"."Users" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405140955_AddFullTextSearch') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260405140955_AddFullTextSearch', '10.0.1');
    END IF;
END $EF$;
COMMIT;

