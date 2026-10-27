using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

#pragma warning disable CA1515, CA1707 // Test types are public for DI and EF; underscores in test names

namespace Common.Tests.Persistence;

public readonly record struct SaveProbeId(DefaultIdType Value) : IStronglyTypedId
{
    public static SaveProbeId New()
    {
        return new SaveProbeId(DefaultIdType.CreateVersion7());
    }
}

public sealed record SaveProbeCreatedDomainEvent(DefaultIdType ProbeId, string Code) : DomainEvent;

public sealed class SaveProbe : AggregateRoot<SaveProbeId>
{
    private SaveProbe() : base(default)
    {
    }

    public string Code { get; private set; } = string.Empty;

    public static SaveProbe Create(string code)
    {
        var probe = new SaveProbe { Id = SaveProbeId.New(), Code = code, CreatedOn = DateTimeOffset.UtcNow };
        probe.RaiseEvent(new SaveProbeCreatedDomainEvent(probe.Id.Value, code));
        return probe;
    }
}

internal sealed class SaveProbeConfiguration : AuditableEntityConfiguration<SaveProbe, SaveProbeId>
{
    public override void Configure(EntityTypeBuilder<SaveProbe> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Code).HasMaxLength(64);
        builder.HasIndex(p => p.Code).IsUnique();
    }
}

/// <summary>A module-shaped context: it derives from the platform base, so the save pipeline under test is the real one.</summary>
public sealed class SaveProbeDbContext(
    DbContextOptions<SaveProbeDbContext> options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : BaseDbContext(options, timeProvider, currentUser, logger, eventDispatcher, integrationEventOutbox)
{
    public const string Schema = "CommonSaveTests";
    private static readonly SemaphoreSlim CreateLock = new(1, 1);
    private static bool _created;

    public DbSet<SaveProbe> Probes => Set<SaveProbe>();

    public static async Task EnsureTablesAsync(SaveProbeDbContext context)
    {
        await CreateLock.WaitAsync();
        try
        {
            if (_created)
            {
                return;
            }

            // EnsureCreated no-ops once any table exists in the database (the modules' migrations ran first), so
            // create this context's tables directly.
            await context.Database.ExecuteSqlRawAsync($"CREATE SCHEMA IF NOT EXISTS \"{Schema}\"");
            await context.GetService<IRelationalDatabaseCreator>().CreateTablesAsync();
            await context.Database.ExecuteSqlRawAsync(
                $"CREATE TABLE IF NOT EXISTS \"{Schema}\".\"HandlerNotes\" (\"Note\" text NOT NULL)");
            _created = true;
        }
        finally
        {
            CreateLock.Release();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
        modelBuilder.ApplyConfiguration(new SaveProbeConfiguration());
    }
}

/// <summary>
///     Writes with SQL of its own, as a handler that cannot go through the change tracker does. A code starting with
///     <see cref="ThrowPrefix" /> makes it collect an integration event and then fail. The host registers every handler of this assembly too, where
///     the probe context does not exist, so the context is resolved when an event arrives, not when the handler is built.
/// </summary>
public sealed class WriteNoteOnSaveProbeCreated(IServiceProvider services) : DomainEventHandlerBase<SaveProbeCreatedDomainEvent>
{
    public const string ThrowPrefix = "throw:";

    public override async Task HandleAsync(SaveProbeCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        await services.GetRequiredService<SaveProbeDbContext>().Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO \"CommonSaveTests\".\"HandlerNotes\" (\"Note\") VALUES ({@event.Code})", cancellationToken);

        if (@event.Code.StartsWith(ThrowPrefix, StringComparison.Ordinal))
        {
            services.GetRequiredService<IIntegrationEventOutbox>().Collect(new TestableIntegrationEvent(@event.Code));
            throw new InvalidOperationException("The handler failed after its write.");
        }
    }
}

public class SaveTransactionTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task SaveChanges_BaseSaveFailsAfterHandlerWroteWithSql_RollsTheHandlerWriteBack()
    {
        await using var provider = await BuildProviderAsync();
        await using var scope = provider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>();
        var code = Unique("dup");
        db.Probes.Add(SaveProbe.Create(code));
        db.Probes.Add(SaveProbe.Create(code));

        await Assert.ThrowsAnyAsync<Exception>(() => db.SaveChangesAsync());

        Assert.Equal(0, await CountNotesAsync(db, code));
        Assert.Equal(0, await db.Probes.AsNoTracking().CountAsync(p => p.Code == code));
        Assert.Null(db.Database.CurrentTransaction);
    }

    [Fact]
    public async Task SaveChanges_Succeeds_CommitsTheChangeAndTheHandlerWriteTogether()
    {
        await using var provider = await BuildProviderAsync();
        await using var scope = provider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>();
        var code = Unique("ok");
        db.Probes.Add(SaveProbe.Create(code));

        await db.SaveChangesAsync();

        Assert.Equal(1, await CountNotesAsync(db, code));
        Assert.Equal(1, await db.Probes.AsNoTracking().CountAsync(p => p.Code == code));
        Assert.Null(db.Database.CurrentTransaction);
    }

    [Fact]
    public async Task SaveChanges_HandlerThrowsAfterItsWrite_RollsEverythingBackAndLeavesNoTransactionOpen()
    {
        await using var provider = await BuildProviderAsync();
        await using var scope = provider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>();
        var code = Unique(WriteNoteOnSaveProbeCreated.ThrowPrefix);
        db.Probes.Add(SaveProbe.Create(code));

        await Assert.ThrowsAsync<InvalidOperationException>(() => db.SaveChangesAsync());

        Assert.Equal(0, await CountNotesAsync(db, code));
        Assert.Null(db.Database.CurrentTransaction);

        // The same context saves again once the failing probe is gone: the failed attempt left no transaction or audit row behind.
        db.ChangeTracker.Clear();
        var next = Unique("after");
        db.Probes.Add(SaveProbe.Create(next));
        await db.SaveChangesAsync();
        Assert.Equal(1, await CountNotesAsync(db, next));
    }

    [Fact]
    public async Task SaveChanges_HandlerCollectsAnIntegrationEventThenThrows_DoesNotLeakItIntoTheNextSave()
    {
        await using var provider = await BuildProviderAsync();
        await using var scope = provider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>();
        db.Probes.Add(SaveProbe.Create(Unique(WriteNoteOnSaveProbeCreated.ThrowPrefix)));
        await Assert.ThrowsAsync<InvalidOperationException>(() => db.SaveChangesAsync());
        db.ChangeTracker.Clear();

        db.Probes.Add(SaveProbe.Create(Unique("after")));
        await db.SaveChangesAsync();

        Assert.Equal(0, await CountOutboxMessagesAsync(db));
    }

    [Fact]
    public async Task SaveChanges_CallerHoldsATransaction_JoinsItAndLeavesTheOutcomeToTheCaller()
    {
        await using var provider = await BuildProviderAsync();
        await using var scope = provider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>();
        var rolledBack = Unique("rollback");
        var committed = Unique("commit");

        await using (var outer = await db.Database.BeginTransactionAsync())
        {
            db.Probes.Add(SaveProbe.Create(rolledBack));
            await db.SaveChangesAsync();

            Assert.Same(outer.GetDbTransaction(), db.Database.CurrentTransaction!.GetDbTransaction());
            Assert.Equal(1, await CountNotesAsync(db, rolledBack));
            await outer.RollbackAsync();
        }

        Assert.Equal(0, await CountNotesAsync(db, rolledBack));
        Assert.Equal(0, await db.Probes.AsNoTracking().CountAsync(p => p.Code == rolledBack));

        await using (var outer = await db.Database.BeginTransactionAsync())
        {
            db.Probes.Add(SaveProbe.Create(committed));
            await db.SaveChangesAsync();
            await outer.CommitAsync();
        }

        Assert.Equal(1, await CountNotesAsync(db, committed));
        Assert.Equal(1, await db.Probes.AsNoTracking().CountAsync(p => p.Code == committed));
    }

    private static string Unique(string prefix)
    {
        return $"{prefix}{Guid.NewGuid():N}";
    }

    private async Task<ServiceProvider> BuildProviderAsync()
    {
        var currentUser = Substitute.For<ICurrentUser>();
        var services = new ServiceCollection()
            .AddLogging()
            .AddSingleton(TimeProvider.System)
            .AddSingleton(currentUser)
            .AddScoped<EventDispatcher>()
            .AddScoped<IntegrationEventOutbox>()
            .AddScoped<IIntegrationEventOutbox>(sp => sp.GetRequiredService<IntegrationEventOutbox>())
            .AddScoped<IEventHandler<SaveProbeCreatedDomainEvent>, WriteNoteOnSaveProbeCreated>()
            .AddDbContext<SaveProbeDbContext>(o => o.UseNpgsql(Factory.ConnectionString).UseExceptionProcessor());
        var provider = services.BuildServiceProvider();

        await using var scope = provider.CreateAsyncScope();
        await SaveProbeDbContext.EnsureTablesAsync(scope.ServiceProvider.GetRequiredService<SaveProbeDbContext>());
        return provider;
    }

    private static Task<int> CountOutboxMessagesAsync(SaveProbeDbContext db)
    {
        return db.Database
            .SqlQuery<int>($"""SELECT count(*)::int AS "Value" FROM "Outbox"."OutboxMessages" """)
            .SingleAsync();
    }

    private static Task<int> CountNotesAsync(SaveProbeDbContext db, string code)
    {
        return db.Database
            .SqlQuery<int>($"""SELECT count(*)::int AS "Value" FROM "CommonSaveTests"."HandlerNotes" WHERE "Note" = {code}""")
            .SingleAsync();
    }
}
