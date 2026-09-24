using Common.Application.Jobs;
using Common.Application.Persistence;
using Common.Application.Persistence.Projections;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Tests.Helpers;

public readonly record struct SampleJobId(DefaultIdType Value) : IStronglyTypedId
{
    public static SampleJobId New()
    {
        return new SampleJobId(DefaultIdType.CreateVersion7());
    }
}

public sealed class SampleJob : JobRow<SampleJobId>
{
#pragma warning disable IDE0051 // EF Core binds this constructor when materializing rows
    private SampleJob(SampleJobId id) : base(id)
    {
    }
#pragma warning restore IDE0051

    public SampleJob(ApplicationUserId? requestedBy, DateTimeOffset queuedOn) : base(SampleJobId.New(), requestedBy, queuedOn)
    {
    }
}

public sealed class SampleProjection : ProjectionEntity
{
    public required string SourceId { get; init; }
    public string Name { get; set; } = string.Empty;
}

internal sealed class SampleJobConfiguration : JobRowConfiguration<SampleJob, SampleJobId>;

internal sealed class SampleProjectionConfiguration : AuditableEntityConfiguration<SampleProjection>
{
    public override void Configure(EntityTypeBuilder<SampleProjection> builder)
    {
        base.Configure(builder);
        builder.HasKey(p => p.SourceId);
        builder.Property(p => p.SourceId).HasMaxLength(64);
        builder.Property(p => p.Name).HasMaxLength(128);
    }
}

/// <summary>
///     Minimal context for exercising the generic Common helpers against real Postgres without a module. Lives in its own
///     schema (not reset by Respawn), so tests use unique keys. Tables are created once per test process.
/// </summary>
public sealed class HelpersTestDbContext(DbContextOptions<HelpersTestDbContext> options) : DbContext(options), IDbContext
{
    public const string Schema = "CommonTests";
    private static readonly SemaphoreSlim CreateLock = new(1, 1);
    private static bool _created;

    public DbSet<AuditLogEntry> AuditLog => Set<AuditLogEntry>();
    public DbSet<SampleJob> Jobs => Set<SampleJob>();
    public DbSet<SampleProjection> Projections => Set<SampleProjection>();

    public static async Task<HelpersTestDbContext> CreateAsync(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<HelpersTestDbContext>()
            .UseNpgsql(connectionString)
            .UseExceptionProcessor();
        var context = new HelpersTestDbContext(builder.Options);

        await CreateLock.WaitAsync();
        try
        {
            if (!_created)
            {
                // EnsureCreated no-ops once any table exists in the database (the modules' migrations ran first), so
                // create this context's tables directly.
                await context.Database.ExecuteSqlRawAsync($"CREATE SCHEMA IF NOT EXISTS \"{Schema}\"");
                await context.GetService<IRelationalDatabaseCreator>().CreateTablesAsync();
                _created = true;
            }
        }
        finally
        {
            CreateLock.Release();
        }

        return context;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
        modelBuilder.ApplyConfiguration(new SampleJobConfiguration());
        modelBuilder.ApplyConfiguration(new SampleProjectionConfiguration());
    }
}
