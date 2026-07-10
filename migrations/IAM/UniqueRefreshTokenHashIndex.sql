START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260710084618_UniqueRefreshTokenHashIndex') THEN
    DROP INDEX "IAM"."IX_RefreshTokens_TokenHash";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260710084618_UniqueRefreshTokenHashIndex') THEN
    CREATE UNIQUE INDEX "IX_RefreshTokens_TokenHash" ON "IAM"."RefreshTokens" ("TokenHash");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260710084618_UniqueRefreshTokenHashIndex') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260710084618_UniqueRefreshTokenHashIndex', '10.0.9');
    END IF;
END $EF$;
COMMIT;

