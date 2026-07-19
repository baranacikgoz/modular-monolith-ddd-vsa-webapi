using Xunit;

// Two WebApplicationFactory-based fixtures live in this assembly (OutboxTestWebAppFactory,
// OutboxProcessorTestFactory) and they cannot share one factory: the backoff tests need the
// OutboxProcessor poller running, the processor tests need it off so direct ProcessBatchAsync
// calls don't race a background poller stealing claims. Parallel factory boots corrupt shared
// global state (Serilog static logger, OTel ActivitySource) — see CLAUDE.md §8 — so run the
// assembly sequentially instead.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
