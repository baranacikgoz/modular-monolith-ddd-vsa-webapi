START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260513131142_RemoveXminConcurrencyToken') THEN
    INSERT INTO "Outbox"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260513131142_RemoveXminConcurrencyToken', '10.0.7');
    END IF;
END $EF$;
COMMIT;

