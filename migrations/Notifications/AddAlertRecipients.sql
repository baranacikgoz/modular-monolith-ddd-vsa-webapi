START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075847_AddAlertRecipients') THEN
    CREATE TABLE "Notifications"."AlertRecipients" (
        "UserId" uuid NOT NULL,
        "Email" character varying(320) NOT NULL,
        "LastSeenOn" timestamp with time zone NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_AlertRecipients" PRIMARY KEY ("UserId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Notifications"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075847_AddAlertRecipients') THEN
    INSERT INTO "Notifications"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027075847_AddAlertRecipients', '10.0.14');
    END IF;
END $EF$;
COMMIT;

