using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityAndAuth.Persistence.EntityConfigurations;

public class ApplicationUserIdConverter : ValueConverter<ApplicationUserId, Guid>
{
    public ApplicationUserIdConverter() : base(
        id => id.Value, // Convert from ApplicationUserId to Guid
        value => new ApplicationUserId(value) // Convert from Guid to ApplicationUserId
    )
    {
    }
}
