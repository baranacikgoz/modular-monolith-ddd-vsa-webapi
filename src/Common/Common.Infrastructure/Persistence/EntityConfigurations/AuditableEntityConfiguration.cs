using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.EntityConfigurations;
public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IAuditableEntity
{
    private const int IpMaxLength = 25; // It sometimes assign weird ip addresses exceeding 15 chars while local development with docker.
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(e => e.CreatedOn)
            .IsRequired();

        builder
            .Property(e => e.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(e => e.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(e => e.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);

        builder
            .Property(e => e.LastModifiedIp)
            .HasMaxLength(IpMaxLength)
            .IsRequired();
    }
}

public abstract class AuditableEntityConfiguration<TEntity, TId> : AuditableEntityConfiguration<TEntity>
    where TId : IStronglyTypedId, new()
    where TEntity : AuditableEntity<TId>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasConversion<StronglyTypedIdValueConverter<TId>>()
            .IsRequired();
    }
}
