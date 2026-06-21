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
    VALUES ('20260403100753_Initial', '10.0.9');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."Stores" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Name" || ' ' || "Description" || ' ' || "Address")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."ProductTemplates" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Brand" || ' ' || "Model" || ' ' || "Color")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    ALTER TABLE "Products"."Products" ADD "SearchVector" tsvector GENERATED ALWAYS AS (to_tsvector('english', "Name" || ' ' || "Description")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_Stores_SearchVector" ON "Products"."Stores" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_ProductTemplates_SearchVector" ON "Products"."ProductTemplates" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    CREATE INDEX "IX_Products_SearchVector" ON "Products"."Products" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260405100359_AddFullSearch') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260405100359_AddFullSearch', '10.0.9');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "Products"."EventStoreEvents" RENAME TO "AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    ALTER TABLE "Products"."AuditLog" RENAME CONSTRAINT "PK_EventStoreEvents" TO "PK_AuditLog";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    DROP INDEX "Products"."IX_EventStoreEvents_AggregateType";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    CREATE INDEX "IX_AuditLog_AggregateId_AggregateType_CreatedOn" ON "Products"."AuditLog" ("AggregateId", "AggregateType", "CreatedOn" DESC);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260407125159_RenameEventStoreEventsToAuditLog') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260407125159_RenameEventStoreEventsToAuditLog', '10.0.9');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    CREATE EXTENSION IF NOT EXISTS unaccent;

    DO $$ BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'simple_unaccent') THEN
            CREATE TEXT SEARCH CONFIGURATION simple_unaccent ( COPY = simple );
            ALTER TEXT SEARCH CONFIGURATION simple_unaccent
                ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple;
        END IF;
    END $$;

    DO $$ BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'english_unaccent') THEN
            CREATE TEXT SEARCH CONFIGURATION english_unaccent ( COPY = english );
            ALTER TEXT SEARCH CONFIGURATION english_unaccent
                ALTER MAPPING FOR hword, hword_part, word WITH unaccent, english_stem;
        END IF;
    END $$;

    DO $$ BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'turkish_unaccent') THEN
            CREATE TEXT SEARCH CONFIGURATION turkish_unaccent ( COPY = turkish );
            ALTER TEXT SEARCH CONFIGURATION turkish_unaccent
                ALTER MAPPING FOR hword, hword_part, word WITH unaccent, turkish_stem;
        END IF;
    END $$;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    CREATE OR REPLACE FUNCTION fts_product(lang text, name text, descr text)
    RETURNS tsvector LANGUAGE sql IMMUTABLE AS $$
      SELECT setweight(to_tsvector('simple_unaccent', coalesce(name,  '')), 'A')
          || setweight(to_tsvector(lang::regconfig,   coalesce(descr, '')), 'B');
    $$;

    CREATE OR REPLACE FUNCTION fts_store(lang text, name text, address text, descr text)
    RETURNS tsvector LANGUAGE sql IMMUTABLE AS $$
      SELECT setweight(to_tsvector('simple_unaccent', coalesce(name,    '')), 'A')
          || setweight(to_tsvector('simple_unaccent', coalesce(address, '')), 'B')
          || setweight(to_tsvector(lang::regconfig,   coalesce(descr,   '')), 'C');
    $$;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Stores" ADD "Language" text NOT NULL DEFAULT 'simple_unaccent';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Products" ADD "Language" text NOT NULL DEFAULT 'simple_unaccent';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    DROP INDEX "Products"."IX_Stores_SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Stores" DROP COLUMN "SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Stores" ADD COLUMN "SearchVector" tsvector GENERATED ALWAYS AS (fts_store("Language", "Name", "Address", "Description")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    CREATE INDEX "IX_Stores_SearchVector" ON "Products"."Stores" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    DROP INDEX "Products"."IX_ProductTemplates_SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."ProductTemplates" DROP COLUMN "SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."ProductTemplates" ADD COLUMN "SearchVector" tsvector GENERATED ALWAYS AS (setweight(to_tsvector('simple_unaccent', coalesce("Brand",'') || ' ' || coalesce("Model",'') || ' ' || coalesce("Color",'')), 'A')) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    CREATE INDEX "IX_ProductTemplates_SearchVector" ON "Products"."ProductTemplates" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    DROP INDEX "Products"."IX_Products_SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Products" DROP COLUMN "SearchVector";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    ALTER TABLE "Products"."Products" ADD COLUMN "SearchVector" tsvector GENERATED ALWAYS AS (fts_product("Language", "Name", "Description")) STORED;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    CREATE INDEX "IX_Products_SearchVector" ON "Products"."Products" USING GIN ("SearchVector");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Products"."__EFMigrationsHistory" WHERE "MigrationId" = '20260619154555_MultilingualSearch') THEN
    INSERT INTO "Products"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260619154555_MultilingualSearch', '10.0.9');
    END IF;
END $EF$;
COMMIT;

