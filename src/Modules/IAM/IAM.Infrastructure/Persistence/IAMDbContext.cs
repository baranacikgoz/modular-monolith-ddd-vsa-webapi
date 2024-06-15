using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EventSourcing;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Persistence;

#pragma warning disable S101 // Types should be named in PascalCase
public class IAMDbContext(
#pragma warning restore S101 // Types should be named in PascalCase
    DbContextOptions<IAMDbContext> options
    ) : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId, IdentityUserClaim<ApplicationUserId>, IdentityUserRole<ApplicationUserId>, IdentityUserLogin<ApplicationUserId>, IdentityRoleClaim<ApplicationUserId>, IdentityUserToken<ApplicationUserId>>(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(nameof(IAM));
        builder.ApplyConfigurationsFromAssembly(typeof(IAMDbContext).Assembly);

        builder.Ignore<DomainEvent>();
        builder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

}
