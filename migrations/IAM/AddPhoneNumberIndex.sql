START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513172507_AddPhoneNumberIndex') THEN
    CREATE INDEX "IX_Users_PhoneNumber" ON "IAM"."Users" ("PhoneNumber");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513172507_AddPhoneNumberIndex') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260513172507_AddPhoneNumberIndex', '10.0.7');
    END IF;
END $EF$;
COMMIT;

