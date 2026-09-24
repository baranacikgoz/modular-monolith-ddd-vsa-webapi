START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923225736_AddProcessedMessagesInbox') THEN
    CREATE TABLE "Products"."ProcessedMessages" (
        "ConsumerName" character varying(256) NOT NULL,
        "MessageId" uuid NOT NULL,
        "ProcessedOn" timestamp with time zone NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_ProcessedMessages" PRIMARY KEY ("ConsumerName", "MessageId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923225736_AddProcessedMessagesInbox') THEN
    CREATE INDEX "IX_ProcessedMessages_ProcessedOn" ON "Products"."ProcessedMessages" ("ProcessedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260923225736_AddProcessedMessagesInbox') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260923225736_AddProcessedMessagesInbox', '10.0.11');
    END IF;
END $EF$;
COMMIT;

