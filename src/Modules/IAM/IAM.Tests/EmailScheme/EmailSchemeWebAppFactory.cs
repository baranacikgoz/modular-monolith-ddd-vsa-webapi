using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace IAM.Tests.EmailScheme;

/// <summary>
///     Same host as <see cref="IntegrationTestWebAppFactory" /> (real Keycloak, in-process OTP fakes) but running
///     the Email identity scheme, so the routes IAMModule maps only for that scheme can be exercised.
/// </summary>
public class EmailSchemeWebAppFactory : IntegrationTestWebAppFactory
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        // Added after the base collection so this value wins. IAMModule reads it through runtime IOptions
        // at MapEndpoints time, so in-memory configuration is enough (no UseSetting needed).
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "IdentitySchemeOptions:Scheme", "Email" }
            });
        });
    }
}

/// <summary>
///     DisableParallelization: this collection runs after the parallel ones finish, so the second host (and its
///     Keycloak container) never boots concurrently with the main IntegrationTestCollection. Two hosts booting in
///     parallel in one assembly corrupt the Serilog/OTel statics (CLAUDE.md §8).
/// </summary>
[CollectionDefinition("EmailSchemeCollection", DisableParallelization = true)]
public class EmailSchemeCollection : ICollectionFixture<EmailSchemeWebAppFactory>
{
}
