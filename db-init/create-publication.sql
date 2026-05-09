CREATE
PUBLICATION dbz_outbox_publication
    FOR TABLE "Outbox"."OutboxMessages", "Outbox"."IntegrationEventOutboxMessages"
    WITH (publish = 'insert');
