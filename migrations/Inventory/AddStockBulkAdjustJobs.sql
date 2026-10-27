START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE TABLE "Inventory"."StockBulkAdjustJobs" (
        "Id" uuid NOT NULL,
        "ClientRequestId" character varying(64) NOT NULL,
        "FileName" character varying(256) NOT NULL,
        "ObjectKey" character varying(512) NOT NULL,
        "Reason" character varying(256) NOT NULL,
        "LastProcessedRow" integer NOT NULL,
        "TotalRows" integer NOT NULL,
        "RowsAdjusted" integer NOT NULL,
        "RowsRejected" integer NOT NULL,
        "RowErrors" jsonb NOT NULL DEFAULT ('[]'::jsonb),
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Status" integer NOT NULL,
        "RequestedBy" uuid,
        "QueuedOn" timestamp with time zone NOT NULL,
        "StartedOn" timestamp with time zone,
        "FinishedOn" timestamp with time zone,
        "FailureKey" character varying(128),
        "Progress" integer NOT NULL,
        CONSTRAINT "PK_StockBulkAdjustJobs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE INDEX "IX_StockBulkAdjustJobs_FinishedOn" ON "Inventory"."StockBulkAdjustJobs" ("FinishedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE INDEX "IX_StockBulkAdjustJobs_RequestedBy_QueuedOn" ON "Inventory"."StockBulkAdjustJobs" ("RequestedBy", "QueuedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE INDEX "IX_StockBulkAdjustJobs_Status_StartedOn" ON "Inventory"."StockBulkAdjustJobs" ("Status", "StartedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE UNIQUE INDEX "UX_StockBulkAdjustJobs_RequestedBy_Active" ON "Inventory"."StockBulkAdjustJobs" ("RequestedBy") WHERE "Status" IN (0, 1);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    CREATE UNIQUE INDEX "UX_StockBulkAdjustJobs_RequestedBy_ClientRequestId" ON "Inventory"."StockBulkAdjustJobs" ("RequestedBy", "ClientRequestId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Inventory"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027095249_AddStockBulkAdjustJobs') THEN
    INSERT INTO "Inventory"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027095249_AddStockBulkAdjustJobs', '10.0.14');
    END IF;
END $EF$;
COMMIT;

