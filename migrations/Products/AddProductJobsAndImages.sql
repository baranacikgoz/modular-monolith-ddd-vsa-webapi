START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE TABLE "Products"."ExportJobs" (
        "Id" uuid NOT NULL,
        "StoreId" uuid,
        "FileName" character varying(128),
        "ObjectKey" character varying(512),
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
        CONSTRAINT "PK_ExportJobs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE TABLE "Products"."ImportJobs" (
        "Id" uuid NOT NULL,
        "StoreId" uuid NOT NULL,
        "ProductTemplateId" uuid NOT NULL,
        "ClientRequestId" character varying(64) NOT NULL,
        "FileName" character varying(256) NOT NULL,
        "ObjectKey" character varying(512) NOT NULL,
        "LastProcessedRow" integer NOT NULL,
        "TotalRows" integer NOT NULL,
        "ProductsCreated" integer NOT NULL,
        "ProductsUpdated" integer NOT NULL,
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
        CONSTRAINT "PK_ImportJobs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE TABLE "Products"."ProductImages" (
        "Id" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "ObjectKey" character varying(512) NOT NULL,
        "ContentType" character varying(64) NOT NULL,
        "SizeBytes" bigint NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_ProductImages" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ProductImages_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products"."Products" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ExportJobs_FinishedOn" ON "Products"."ExportJobs" ("FinishedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ExportJobs_RequestedBy_QueuedOn" ON "Products"."ExportJobs" ("RequestedBy", "QueuedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ExportJobs_Status_StartedOn" ON "Products"."ExportJobs" ("Status", "StartedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE UNIQUE INDEX "UX_ExportJobs_RequestedBy_Active" ON "Products"."ExportJobs" ("RequestedBy") WHERE "Status" IN (0, 1);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ImportJobs_FinishedOn" ON "Products"."ImportJobs" ("FinishedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ImportJobs_RequestedBy_QueuedOn" ON "Products"."ImportJobs" ("RequestedBy", "QueuedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE INDEX "IX_ImportJobs_Status_StartedOn" ON "Products"."ImportJobs" ("Status", "StartedOn");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE UNIQUE INDEX "UX_ImportJobs_RequestedBy_Active" ON "Products"."ImportJobs" ("RequestedBy") WHERE "Status" IN (0, 1);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE UNIQUE INDEX "UX_ImportJobs_RequestedBy_ClientRequestId" ON "Products"."ImportJobs" ("RequestedBy", "ClientRequestId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    CREATE UNIQUE INDEX "IX_ProductImages_ProductId_ObjectKey" ON "Products"."ProductImages" ("ProductId", "ObjectKey");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20261027075737_AddProductJobsAndImages') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261027075737_AddProductJobsAndImages', '10.0.14');
    END IF;
END $EF$;
COMMIT;

