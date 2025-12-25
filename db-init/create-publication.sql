CREATE
PUBLICATION dbz_outbox_publication
    FOR TABLE "Outbox"."OutboxMessages"
    WITH (publish = 'insert');
