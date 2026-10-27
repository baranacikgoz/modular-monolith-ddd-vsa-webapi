START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090414_AddWebhookDeliveries') THEN
    CREATE TABLE "Inventory"."WebhookDeliveries" (
        "Id" uuid NOT NULL,
        "ConnectionId" uuid NOT NULL,
        "DeliveryId" character varying(128) NOT NULL,
        "ReservationId" uuid NOT NULL,
        "ReceivedOn" timestamp with time zone NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_WebhookDeliveries" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090414_AddWebhookDeliveries') THEN
    CREATE UNIQUE INDEX "IX_WebhookDeliveries_ConnectionId_DeliveryId" ON "Inventory"."WebhookDeliveries" ("ConnectionId", "DeliveryId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090414_AddWebhookDeliveries') THEN
    CREATE INDEX "IX_WebhookDeliveries_ReceivedOn" ON "Inventory"."WebhookDeliveries" ("ReceivedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027090414_AddWebhookDeliveries') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027090414_AddWebhookDeliveries', '10.0.14');
    END IF;
END $EF$;
COMMIT;

