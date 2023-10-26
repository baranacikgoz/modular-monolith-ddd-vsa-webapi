using IdentityAndAuth.Auth;
using IdentityAndAuth.Identity;
using IdentityAndAuth.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Linq;
using IdentityAndAuth.Features.Users.Domain;

namespace IdentityAndAuth.Persistence.Seeding;

internal sealed partial class Seeder(ILogger<Seeder> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
{
    public async Task SeedDb()
    {
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }
}
