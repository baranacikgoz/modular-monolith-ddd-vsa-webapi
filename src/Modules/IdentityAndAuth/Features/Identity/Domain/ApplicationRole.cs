using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Features.Identity.Domain;

internal class ApplicationRole : IdentityRole<Guid>
{
    public string? Description { get; set; }

    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}
