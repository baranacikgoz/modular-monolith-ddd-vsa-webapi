using Common.Core.Contracts;
using System.Reflection.Emit;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Common.Persistence.EventSourcing;

namespace IdentityAndAuth.Persistence;

public class IdentityDbContext(
    DbContextOptions<IdentityDbContext> options
    ) : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId, IdentityUserClaim<ApplicationUserId>, IdentityUserRole<ApplicationUserId>, IdentityUserLogin<ApplicationUserId>, IdentityRoleClaim<ApplicationUserId>, IdentityUserToken<ApplicationUserId>>(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(nameof(IdentityAndAuth));
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

        builder.Ignore<DomainEvent>();
        builder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

}
