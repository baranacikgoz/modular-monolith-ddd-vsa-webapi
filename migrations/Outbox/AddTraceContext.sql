START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511115622_AddTraceContext') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ADD "ParentSpanId" character varying(16);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511115622_AddTraceContext') THEN
    ALTER TABLE "Outbox"."OutboxMessages" ADD "TraceId" character varying(32);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "Outbox"."__EFMigrationsHistory" WHERE "MigrationId" = '20260511115622_AddTraceContext') THEN
    INSERT INTO "Outbox"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260511115622_AddTraceContext', '10.0.7');
    END IF;
END $EF$;
COMMIT;

