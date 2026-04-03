DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Products') THEN
        CREATE SCHEMA "Products";
    END IF;
END $EF$;
CREATE TABLE IF NOT EXISTS "Products"."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Products') THEN
            CREATE SCHEMA "Products";
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE TABLE "Products"."EventStoreEvents" (
        "AggregateId" uuid NOT NULL,
        "Version" bigint NOT NULL,
        "AggregateType" character varying(128) NOT NULL,
        "EventType" character varying(256) NOT NULL,
        "Event" jsonb NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_EventStoreEvents" PRIMARY KEY ("AggregateId", "Version")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE TABLE "Products"."ProductTemplates" (
        "Id" uuid NOT NULL,
        "IsActive" boolean NOT NULL,
        "Brand" character varying(100) NOT NULL,
        "Model" character varying(2000) NOT NULL,
        "Color" character varying(100) NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        CONSTRAINT "PK_ProductTemplates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE TABLE "Products"."Stores" (
        "Id" uuid NOT NULL,
        "OwnerId" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Description" character varying(1000) NOT NULL,
        "Address" character varying(1000) NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Version" bigint NOT NULL,
        CONSTRAINT "PK_Stores" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE TABLE "Products"."Products" (
        "Id" uuid NOT NULL,
        "StoreId" uuid NOT NULL,
        "ProductTemplateId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text NOT NULL,
        "Quantity" integer NOT NULL,
        "Price" numeric NOT NULL,
        "CreatedOn" timestamp with time zone NOT NULL,
        "CreatedBy" uuid,
        "LastModifiedOn" timestamp with time zone,
        "LastModifiedBy" uuid,
        "Version" bigint NOT NULL,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Products_ProductTemplates_ProductTemplateId" FOREIGN KEY ("ProductTemplateId") REFERENCES "Products"."ProductTemplates" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Products_Stores_StoreId" FOREIGN KEY ("StoreId") REFERENCES "Products"."Stores" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE INDEX "IX_EventStoreEvents_AggregateType" ON "Products"."EventStoreEvents" ("AggregateType");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE INDEX "IX_Products_ProductTemplateId" ON "Products"."Products" ("ProductTemplateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE INDEX "IX_Products_StoreId" ON "Products"."Products" ("StoreId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE INDEX "IX_ProductTemplates_IsActive" ON "Products"."ProductTemplates" ("IsActive");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    CREATE UNIQUE INDEX "IX_Stores_OwnerId" ON "Products"."Stores" ("OwnerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260403100753_Initial') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260403100753_Initial', '10.0.1');
    END IF;
END $EF$;
COMMIT;

