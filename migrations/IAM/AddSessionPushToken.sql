START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260729173117_AddSessionPushToken') THEN
    ALTER TABLE "IAM"."Sessions" ADD "PushToken" character varying(4096);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260729173117_AddSessionPushToken') THEN
    ALTER TABLE "IAM"."Sessions" ADD "PushTokenUpdatedOn" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "IAM"."__EFMigrationsHistory" WHERE "MigrationId" = '20260729173117_AddSessionPushToken') THEN
    INSERT INTO "IAM"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260729173117_AddSessionPushToken', '10.0.10');
    END IF;
END $EF$;
COMMIT;

