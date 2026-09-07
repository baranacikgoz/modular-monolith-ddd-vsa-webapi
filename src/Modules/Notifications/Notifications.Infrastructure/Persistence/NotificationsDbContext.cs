using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notifications.Application.Persistence;
using Notifications.Domain.Devices;

namespace Notifications.Infrastructure.Persistence;

public sealed class NotificationsDbContext(
    DbContextOptions<NotificationsDbContext> options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : BaseDbContext(options, timeProvider, currentUser, logger, eventDispatcher, integrationEventOutbox),
    INotificationsDbContext
{
    public DbSet<DeviceRegistration> DeviceRegistrations => Set<DeviceRegistration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Notifications));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
    }
}
